using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public enum GroundInfoEnum
    {
        notGrounded,
        GroundedOnBasicGround,
        GroundedOnIceGround,
        TochedToWall
    }
    public GroundInfoEnum myGroundInfo;
    public GameObject myPlayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {//ice veya ground'ın 2 tarafı da duvar efeği göstermeli
        myGroundInfo = statusControl(collision.collider.tag[0]);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        myGroundInfo = GroundInfoEnum.notGrounded;
    }

    private GroundInfoEnum statusControl(char compChar)
    {
        if (compChar == 'B')
            return GroundInfoEnum.GroundedOnBasicGround;
        else if (compChar == 'I')
            return GroundInfoEnum.GroundedOnIceGround;
        else if (compChar == 'W')
            return GroundInfoEnum.TochedToWall;
        else
            return GroundInfoEnum.notGrounded;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myPlayer.transform.position += Vector3.up;
        }
    }
}