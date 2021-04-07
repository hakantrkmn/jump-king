using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public sideInfo touchedSideInfo = sideInfo.UNDEFINED;
    public collisionInfo PlayerCollisionInfo;
    public bool isPlayerProperlyOnPlatform;


    public Vector3 normal;

    private void Update()
    {
        if (PlayerCollisionInfo == collisionInfo.noCollision)
        {
            touchedSideInfo = sideInfo.UNDEFINED;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        normal = collision.contacts[0].normal;
        PlayerCollisionInfo = statusControl(collision.collider);

        touchedSideInfo = sideControl(collision.contacts[0].normal);
        isPlayerProperlyOnPlatform = edgeControl();
        if (PlayerCollisionInfo == collisionInfo.CollidingWithBasicGround && touchedSideInfo == sideInfo.up)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerCollisionInfo = collisionInfo.noCollision;
    }
    private bool edgeControl()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Platform"));
        if (hit.collider == null)
            return false;
        return true;
    }

    private collisionInfo statusControl(Collider2D compChar)
    {
        if (compChar.CompareTag("RegularGround"))
            return collisionInfo.CollidingWithBasicGround;
        else if (compChar.CompareTag("GrassGround"))
            return collisionInfo.CollidingWithIceGround;
        else if (compChar.CompareTag("IceGround"))
            return collisionInfo.CollidingWithStar;
        return collisionInfo.noCollision;
    }
    private sideInfo sideControl(Vector3 normal)
    {

        var sideValue = sideInfo.UNDEFINED;
        float normalX = normal.x;
        float normalY = normal.y;
        if (normalX == 1 && normalY==0)
            sideValue= sideInfo.right;
        else if (normalX == -1 && normalY==0)
            sideValue= sideInfo.left;
        else if (normalY == 1 && normalX == 0)
            sideValue = sideInfo.up;
        else if (normalY == -1 && normalX==0)
            sideValue = sideInfo.down;
        else
            sideValue = sideInfo.capraz;
        return sideValue;
    }

    public IEnumerator test()
    {
        yield return new WaitForSeconds(0.2f);
    }

}