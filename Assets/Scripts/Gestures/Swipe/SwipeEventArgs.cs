using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirections
{
    RIGHT,
    LEFT,
    UP,
    DOWN
}

public class SwipeEventArgs : EventArgs
{
    //Position of the swipe
    private Vector2 swipePos;
    //General direction of the swipe
    private SwipeDirections swipeDirection;
    //Raw direction of the swipe
    private Vector2 swipeVector;
    //Hit objecr at start
    private GameObject hitObject;

    public SwipeEventArgs (Vector2 _swipePos, SwipeDirections _swipeDir, Vector2 _swipeVector, GameObject _hitObject)
    {
        swipePos = _swipePos;
        swipeDirection = _swipeDir;
        swipeVector = _swipeVector;
        hitObject = _hitObject;
    }

    public Vector2 SwipePos
    {
        get { return swipePos; }
    }

    public SwipeDirections SwipeDirection
    {
        get { return swipeDirection; }
    }

    public Vector2 SwipeVector
    {
        get { return swipeVector; }
    }

    public GameObject HitObject
    {
        get { return hitObject; }
    }

}
