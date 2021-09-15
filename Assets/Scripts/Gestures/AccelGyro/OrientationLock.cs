using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationLock : MonoBehaviour
{
    DeviceOrientation lastOrientation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastOrientation = Input.deviceOrientation;
    }

    public void OnFingerDown()
    {
        //Cast the last orientation as int
        int orientation = (int)lastOrientation;
        //Since 5 above are Face-up/down just  assign landscape
        if (orientation > 4) orientation = (int)DeviceOrientation.LandscapeLeft;

        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = (ScreenOrientation)orientation;
    }

    public void OnFingerUp()
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;

        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
