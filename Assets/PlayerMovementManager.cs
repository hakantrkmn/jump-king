using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    float power;
    public float jumpPower;
    Vector2 fingerPoint;
    Vector2 jumpVector;
    float xAngle;

    CollisionManager _PlayerCollisionManager;
    Rigidbody2D _PlayerRigidBody;
    InputManager _InputManager;
    void Start()
    {
        _PlayerCollisionManager = GetComponent<CollisionManager>();
        _PlayerRigidBody = GetComponent<Rigidbody2D>();
        _InputManager = GetComponent<InputManager>();
        power = 0;
    }



    void Update()
    {
        setBoundariesforPlayerPosition();
        collisionInfo PlayerCollisionInfo = _PlayerCollisionManager.PlayerCollisionInfo;
        Vector2 PlayerVelocity=_PlayerRigidBody.velocity;
        touchStatus inputStatus = _InputManager.currentTouchStatus;

        bool isPlayerStill = (PlayerCollisionInfo != collisionInfo.noCollision && Mathf.Abs(PlayerVelocity.x) < 0.2);

        if (isPlayerStill)
        {

            if (Input.GetMouseButtonDown(0))
            {

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
                    else if (xAngle > 75)
                    {
                        jumpVector = new Vector2(-0.25f, 1.0f);
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
                        jumpVector = new Vector2(0.25f, 1.0f);
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

                _PlayerRigidBody.AddForce(jumpVector * power * jumpPower);
                power = 0;
            }
        }
        else
        {
            if (_PlayerRigidBody.velocity.y == 0)
            {
                if (_PlayerRigidBody.velocity.magnitude > 0.5f)
                {
                    _PlayerRigidBody.velocity = _PlayerRigidBody.velocity.normalized * 0.5f;
                }
            }
            else if (_PlayerRigidBody.velocity.y < 0)
            {
                _PlayerRigidBody.velocity = _PlayerRigidBody.velocity * 1.01f;
            }
        }

    }
   
    void setBoundariesforPlayerPosition()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.05f, 3.05f), transform.position.y, transform.position.z);
    }
}
