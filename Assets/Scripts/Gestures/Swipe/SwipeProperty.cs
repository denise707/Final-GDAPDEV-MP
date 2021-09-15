using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwipeProperty
{
    //Minimum distance covered to be considered a swipe
    public float minSwipeDistance = 2f;
    //Max gesture time until its not a swipe anymore
    public float swipeTime = 0.7f;
}
