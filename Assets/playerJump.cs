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

    bool animationTrigger;
    public Sprite player_0;
    public Sprite player_1;
    public Sprite player_2;
    public Sprite player_3;
    public Sprite player_4;
    public Sprite player_5;
    public Sprite player_6;
    // Start is called before the first frame update
    void Start()
    {
        animationTrigger = true;
        power = 0;
    }


    IEnumerator jumpBegin()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = player_0;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = player_1;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = player_2;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = player_3;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator jumpAir()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = player_4;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().sprite = player_5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "star")
        {
            Time.timeScale = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity.x);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.45f, 2.45f), transform.position.y, transform.position.z);
        if (gameObject.GetComponent<CollisionManager>().myGroundInfo != CollisionManager.GroundInfoEnum.notGrounded && Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) < 0.2 )
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y == 0 && animationTrigger)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = player_0;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            }

            if (Input.GetMouseButtonDown(0))
            {
                animationTrigger = false;
                StartCoroutine(jumpBegin());
                fingerPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                jumpVector = (fingerPoint - (Vector2)transform.position).normalized;
                xAngle = Vector2.Angle(jumpVector, new Vector2(jumpVector.x, 0));
                if (fingerPoint.x < transform.position.x)
                {
                    gameObject.transform.localScale = new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
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
                    gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
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
                if (power > 1.5f)
                {
                    power = 1.5f;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopCoroutine(jumpBegin());
                StartCoroutine(jumpAir());
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
            else if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                StopCoroutine(jumpAir());
                gameObject.GetComponent<SpriteRenderer>().sprite = player_6;
                animationTrigger = true;
            }
        }
        
    }
}
