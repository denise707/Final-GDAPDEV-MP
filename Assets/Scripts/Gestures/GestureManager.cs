using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance;

    public TapProperty _tapProperty;
    public EventHandler<TapEventArgs> OnTap;

    public SwipeProperty _swipeProperty;
    public EventHandler<SwipeEventArgs> OnSwipe;

    public DragProperty _dragProperty;
    public EventHandler<DragEventArgs> OnDrag;

    public TwoFingerPanProperty _twoFingerPanProperty;
    public EventHandler<TwoFingerPanEventArgs> OnTwoFingerPan;

    public SpreadProperty _spreadProperty;
    public EventHandler<SpreadEventArgs> OnSpread;

    public RotateProperty _rotateProperty;
    public EventHandler<RotateEventArgs> OnRotate;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            //Assign this instance of Gesture Manager
            Instance = this;
        }
        else
        {
            //Destroy other duplicates in scene
            Destroy(gameObject);
        }

    }

    //Track first finger
    Touch trackedFinger1;
    //Track second finger
    Touch trackedFinger2;
    //Start point of the finger gesture
    private Vector2 startPoint = Vector2.zero;
    //End point of the finger gesture
    private Vector2 endPoint = Vector2.zero;
    //Total time of the gesture
    private float gestureTime = 0;

    //For gestures launched in TouchPhase.End
    bool pinched = false;
    float distance = 0.0f;

    bool rotated = false;
    Vector2 _diffVector = Vector2.zero;
    Vector2 _prevDiffVector = Vector2.zero;
    float _angle = 0.0f;

    bool swipped = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                //Single finger checks
                CheckSingleFingerGestures();
            }
            if (Input.touchCount > 1)
            {
                //Assign the tracked fingers
                trackedFinger1 = Input.GetTouch(0);
                trackedFinger2 = Input.GetTouch(1);

                //Rotate Check
                //if ((trackedFinger1.phase == TouchPhase.Moved ||
                //    trackedFinger2.phase == TouchPhase.Moved) &&
                //    (Vector2.Distance(trackedFinger1.position, trackedFinger2.position) >=
                //    (_rotateProperty.minDistance * Screen.dpi)))
                //{
                //    //Get the positions of the finger in the previous frame
                //    Vector2 prevPoint1 = GetPreviousPoint(trackedFinger1);
                //    Vector2 prevPoint2 = GetPreviousPoint(trackedFinger2);

                //    //Get the distance
                //    Vector2 diffVector = trackedFinger1.position - trackedFinger2.position;
                //    Vector2 prevDiffVector = prevPoint1 - prevPoint2;

                //    float angle = Vector2.Angle(prevDiffVector, diffVector);
                //    //Check if angle is more than reuquired amount
                //    if (angle >= _rotateProperty.minChange)
                //    {
                //        //FireRotateEvent(diffVector, prevDiffVector, angle);
                //        rotated = true;
                //        _diffVector = diffVector;
                //        _prevDiffVector = prevDiffVector;
                //        _angle = angle;
                //    }
                //}

                //Spread Check
                if (trackedFinger1.phase == TouchPhase.Moved || trackedFinger2.phase == TouchPhase.Moved)
                {
                    //Get the positions of the finger in the previous frame
                    Vector2 prevPoint1 = GetPreviousPoint(trackedFinger1);
                    Vector2 prevPoint2 = GetPreviousPoint(trackedFinger2);

                    //Get the distance
                    float currDistance = Vector2.Distance(trackedFinger1.position, trackedFinger2.position);
                    float prevDistance = Vector2.Distance(prevPoint1, prevPoint2);

                    //Debug.Log($"DistanceMin: {Math.Abs(currDistance - prevDistance)}");

                    if (Math.Abs(currDistance - prevDistance) >= (_spreadProperty.MinDistanceChange * Screen.dpi))
                    {
                        //FireSpreadEvent(currDistance - prevDistance);
                        //Debug.Log("Pinch true");
                        pinched = true;
                        distance = currDistance - prevDistance;
                    }
                }

                if (trackedFinger1.phase == TouchPhase.Ended || trackedFinger2.phase == TouchPhase.Ended)
                {
                    //Debug.Log("Pinch Ended");
                    if (rotated)
                    {
                        FireRotateEvent(_diffVector, _prevDiffVector, _angle);
                        rotated = false;
                    }
                    else if (pinched)
                    {
                        FireSpreadEvent(distance);
                        pinched = false;
                    }
                }
            }
        }
    }

    private Vector2 GetPreviousPoint(Touch finger)
    {
        //Subtract with delta to get the prev position
        return finger.position - finger.deltaPosition;
    }

    void CheckSingleFingerGestures()
    {
        //Assign the tracked finger
        trackedFinger1 = Input.GetTouch(0);

        if (trackedFinger1.phase == TouchPhase.Began)
        {
            startPoint = trackedFinger1.position;
            gestureTime = 0;
            swipped = true;
        }
        else if (trackedFinger1.phase == TouchPhase.Ended && swipped)
        {
            endPoint = trackedFinger1.position;

            //Tap Check
            if (gestureTime <= _tapProperty.tapTime && Vector2.Distance(startPoint, endPoint) < (Screen.dpi * _tapProperty.tapMaxDistance))
            {
                //Fire the Event using the current position of the finger
                //You can also use the start and end points
                FireTapEvent(trackedFinger1.position);
            }

            //Swipe Check
            else if (gestureTime <= _swipeProperty.swipeTime && Vector2.Distance(startPoint, endPoint) >= (Screen.dpi * _swipeProperty.minSwipeDistance))
            {
                FireSwipeEvent();
            }
            swipped = false;
        }
        else
        {
            gestureTime += Time.deltaTime;

            //Drag Check
            if (gestureTime >= _dragProperty.bufferTime)
            {
                FireDragEvent();
            }
        }
    }

    private void FireTapEvent(Vector2 pos)
    {
        //Debug.Log("Tap");
        //Check if anything is listening first
        if (OnTap != null)
        {
            GameObject hitObj = null;
            Ray r = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                //If we tap something, assign it to the hitObj
                hitObj = hit.collider.gameObject;
            }

            //Create tap event args
            TapEventArgs tapArgs = new TapEventArgs(pos, hitObj);
            //Send the event with THIS object as sender
            //Along with the args
            OnTap(this, tapArgs);

            if (hitObj != null)
            {
                ITap tap = hitObj.GetComponent<ITap>();
                //Call the hit obj's OnTap
                if (tap != null) tap.OnTap();
            }
        }
    }

    private void FireSwipeEvent()
    {
        //Debug.Log("Swipe");
        Vector2 diff = endPoint - startPoint;

        Ray r = Camera.main.ScreenPointToRay(startPoint);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            //If we tap something, assign it to the hitObj
            hitObj = hit.collider.gameObject;
        }

        SwipeDirections swipeDir = SwipeDirections.RIGHT;

        //Check which axis changed the most x or y?
        //Get swipe direction
        if (Math.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            if (diff.x > 0)
            {
                //Debug.Log("Right");
                swipeDir = SwipeDirections.RIGHT;
            }
            else
            {
                //Debug.Log("Left");
                swipeDir = SwipeDirections.LEFT;
            }
        }
        else
        {
            if (diff.y > 0)
            {
                //Debug.Log("Up");
                swipeDir = SwipeDirections.UP;
            }
            else
            {
                //Debug.Log("Down");
                swipeDir = SwipeDirections.DOWN;
            }
        }

        //Create the args for convenience
        SwipeEventArgs args = new SwipeEventArgs(startPoint, swipeDir, diff, hitObj);
        if (OnSwipe != null)
        {
            //Fire the OnSwipe event
            OnSwipe(this, args);
        }

        if (hitObj != null)
        {
            ISwipped swipe = hitObj.GetComponent<ISwipped>();
            if (swipe != null)
            {
                swipe.OnSwipe(args);
            }
        }
    }

    private void FireDragEvent()
    {
        //Debug.Log($"Drag: {trackedFinger1.position}");

        Ray r = Camera.main.ScreenPointToRay(trackedFinger1.position);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        DragEventArgs args = new DragEventArgs(trackedFinger1, hitObj);
        if (OnDrag != null)
        {
            OnDrag(this, args);
        }

        if (hitObj != null)
        {
            IDragged drag = hitObj.GetComponent<IDragged>();
            if (drag != null)
            {
                drag.OnDrag(args);
            }
        }
    }

    private void FireTwoFingerPanEvent()
    {
        //Debug.Log("Two finger pan");

        TwoFingerPanEventArgs args = new TwoFingerPanEventArgs(trackedFinger1, trackedFinger2);

        if (OnTwoFingerPan != null)
        {
            OnTwoFingerPan(this, args);
        }
    }

    private void FireSpreadEvent(float dist)
    {
        if (dist > 0)
        {
            //Debug.Log("Spread");
        }
        else
        {
            //Debug.Log("Pinch");
        }

        //Debug.Log($"Distance Pinch: {dist}");

        Vector2 mid = Midpoint(trackedFinger1.position, trackedFinger2.position);

        Ray r = Camera.main.ScreenPointToRay(mid);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        SpreadEventArgs args = new SpreadEventArgs(trackedFinger1, trackedFinger2, dist, hitObj);

        if (OnSpread != null)
        {
            OnSpread(this, args);
        }

        if (hitObj != null)
        {
            ISpread spread = hitObj.GetComponent<ISpread>();
            if (spread != null)
            {
                spread.OnSpread(args);
            }
        }
    }

    private Vector2 Midpoint(Vector2 p1, Vector2 p2)
    {
        return (p1 + p2) / 2;
    }

    void FireRotateEvent(Vector2 diffVector, Vector2 prevDiffVector, float angle)
    {
        Vector3 cross = Vector3.Cross(prevDiffVector, diffVector);
        RotationDirections rotDir = RotationDirections.CW;
        if (cross.z > 0)
        {
            //Debug.Log($"Rotate CCW {angle}");
            rotDir = RotationDirections.CCW;
        }
        else
        {
            //Debug.Log($"Rotate CW {angle}");
            rotDir = RotationDirections.CW;
        }

        Vector2 mid = Midpoint(trackedFinger1.position, trackedFinger2.position);

        Ray r = Camera.main.ScreenPointToRay(mid);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        RotateEventArgs args = new RotateEventArgs(trackedFinger1, trackedFinger2, angle, rotDir, hitObj);

        if (OnRotate != null)
        {
            OnRotate(this, args);
        }

        if (hitObj != null)
        {
            IRotate rotate = hitObj.GetComponent<IRotate>();
            if (rotate != null)
            {
                rotate.OnRotate(args);
            }
        }
    }
}
