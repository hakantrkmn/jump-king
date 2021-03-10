using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 currentTouchPos2D;
    public touchStatus currentTouchStatus = touchStatus.notTouching;
    public float touchHoldTime = 0f;
    Vector2 TouchStartPos2D;
    public Vector2 TouchEndPos2D;
    Vector2 TouchStartPointToEndPoint2D;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))//Input.GetTouch ile yapılacak normalde
        {
            currentTouchStatus = touchStatus.touchStarted;
            TouchStartPos2D= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentTouchPos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchHoldTime = 0f;
        }
        else if (Input.GetMouseButton(0))
        {
            currentTouchStatus = touchStatus.touchOnScreen;
            currentTouchPos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchHoldTime += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentTouchStatus = touchStatus.touchReleased;
            currentTouchPos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TouchEndPos2D= Camera.main.ScreenToWorldPoint(Input.mousePosition);

            TouchStartPointToEndPoint2D = TouchEndPos2D - TouchStartPos2D;
        }
        else
        {
            currentTouchStatus = touchStatus.notTouching;
        }
        //Debug.Log("Touch Position: "+touchPos2D+" touchStatusCode: "+currentTouchStatus);
    }

}
