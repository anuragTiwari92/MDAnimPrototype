using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour {
    private const float SIGNIFICANTSWIPE = 100.0f;

    public static MobileInput Instance { set; get; }

    private bool tap, swipeL, swipeR, swipeU, swipeD;
    private Vector2 swipeDelta, startTouch;

    public bool TAP { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeL; } }
    public bool SwipeRight { get { return swipeR; } }
    public bool SwipeUp { get { return swipeU; } }
    public bool SwipeDown { get { return swipeD; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //init all bolleans
        tap = swipeL = swipeR = swipeU = swipeD = false;

        //standaloneinputs
        if(Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }

        //mobileinputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }
        }


        //swipe delta calculation
        swipeDelta = Vector2.zero;
        if(startTouch != Vector2.zero)
        {
            if(Input.touches.Length!=0)
            {
                swipeDelta = Input.touches[0].position - startTouch;

            }
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition-startTouch;

            }
        }

        if(swipeDelta.magnitude > SIGNIFICANTSWIPE)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(x)>Mathf.Abs(y))
            {
                if (x < 0)
                    swipeL = true;
                else
                    swipeR = true;
            }
            else
            {
                if (y < 0)
                    swipeD = true;
                else
                    swipeU = true;
            }
            startTouch = swipeDelta = Vector2.zero;
        }
    }



}
