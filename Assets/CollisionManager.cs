using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public sideInfo touchedSideInfo = sideInfo.UNDEFINED;
    public collisionInfo PlayerCollisionInfo;
    public bool isPlayerProperlyOnPlatform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCollisionInfo = statusControl(collision.collider);
        touchedSideInfo = sideControl(collision.contacts[0].normal);
        isPlayerProperlyOnPlatform = edgeControl();

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
        float normalX = normal.x;
        float normalY = normal.y;
        if (normalX > 0)
            return sideInfo.right;
        else if (normalX < 0)
            return sideInfo.left;
        else if (normalY > 0)
            return sideInfo.up;
        else if (normalY < 0)
            return sideInfo.down;
        return sideInfo.UNDEFINED;
    }

}