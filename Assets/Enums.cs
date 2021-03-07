using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum collisionInfo
{
    noCollision,
    CollidingWithBasicGround,
    CollidingWithIceGround,
    CollidingWithStar,
    UNDEFINED
}
public enum touchStatus
{
    touchStarted,
    touchOnScreen,
    touchReleased,
    notTouching
}
public enum sideInfo
{
    right,
    left,
    up,
    down,
    UNDEFINED
}