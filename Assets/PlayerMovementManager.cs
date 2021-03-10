using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
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
    }



    void Update()
    {
        setBoundariesforPlayerPosition();
        collisionInfo PlayerCollisionInfo = _PlayerCollisionManager.PlayerCollisionInfo;
        Vector2 PlayerVelocity = _PlayerRigidBody.velocity;
        touchStatus inputStatus = _InputManager.currentTouchStatus;

        bool isPlayerStill = (PlayerCollisionInfo != collisionInfo.noCollision && Mathf.Abs(PlayerVelocity.x) < 0.2);

        if (isPlayerStill)
        {
            if (inputStatus == touchStatus.touchReleased)
            {
                Vector2 jumpDirectionVector = calculateJumpDirectionVector(_InputManager.TouchEndPos2D, _InputManager.touchHoldTime);
                Debug.Log(jumpDirectionVector);
                _PlayerRigidBody.AddForce(jumpDirectionVector * jumpPower);

            }
        }

    }

    private Vector2 calculateJumpDirectionVector(Vector2 touchEndPosition, float touchHoldTime)
    {
        if (touchHoldTime>1.5f)//min ve max zıplama bunun ile ayarlanacak
            touchHoldTime = 1.5f;
        else if (touchHoldTime<0.3f)
            touchHoldTime = 0.3f;

        fingerPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        jumpVector = (fingerPoint - (Vector2)transform.position).normalized;
        xAngle = Vector2.Angle(jumpVector, new Vector2(jumpVector.x, 0));

        Debug.Log(xAngle);

        if (fingerPoint.x < transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            if (xAngle < 30)
            {
                return jumpVector = new Vector2(-0.8f, 0.6f)*touchHoldTime;
            }
            else if (xAngle > 75)
            {
                return jumpVector = new Vector2(-0.25f, 1.0f)* touchHoldTime;
            }
        }
        else if (fingerPoint.x > transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            if (xAngle < 30)
            {
                return jumpVector = new Vector2(0.8f, 0.6f)* touchHoldTime;
            }
            else if (xAngle > 75)
            {
                return jumpVector = new Vector2(0.25f, 1.0f)* touchHoldTime;
            }
        }

        Debug.Log("Here");
        return jumpVector*touchHoldTime;
    }

    void setBoundariesforPlayerPosition()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.05f, 3.05f), transform.position.y, transform.position.z);
    }
}
