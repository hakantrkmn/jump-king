using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    float power;
    public float jumpPower;
    Vector2 fingerPoint;
    Vector2 jumpVector;
    float xAngle;
    // Start is called before the first frame update
    void Start()
    {
        power = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<CollisionManager>().myGroundInfo != CollisionManager.GroundInfoEnum.notGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                fingerPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                jumpVector = (fingerPoint - (Vector2)transform.position).normalized;
                Debug.Log(jumpVector);
                xAngle = Vector2.Angle(jumpVector, new Vector2(jumpVector.x, 0));
                Debug.Log(xAngle);
                if (fingerPoint.x < transform.position.x)
                {
                    if (xAngle < 30)
                    {
                        jumpVector = new Vector2(-0.8f, 0.6f);
                    }
                    else if (xAngle>75)
                    {
                        jumpVector = new Vector2(-0.3f, 1.0f);
                    }
                }
                else if (fingerPoint.x > transform.position.x)
                {
                    if (xAngle < 30)
                    {
                        jumpVector = new Vector2(0.8f, 0.6f);
                    }
                    else if (xAngle > 75)
                    {
                        jumpVector = new Vector2(0.3f, 1.0f);
                    }
                }
                
            }
            else if (Input.GetMouseButton(0))
            {
                power += Time.deltaTime;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(jumpVector * power * jumpPower);
                power = 0;
            }
        }
        else 
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.5f)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity.normalized * 0.5f;
                }
            }
        }
        
    }
}
