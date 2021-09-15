using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragEventArgs : EventArgs
{
    //Current state of the finger
    private Touch targetFinger;
    //Hit object
    private GameObject hitObject;

    public DragEventArgs(Touch _targetFinger, GameObject _hitObject)
    {
        targetFinger = _targetFinger;
        hitObject = _hitObject;
    }

    public Touch TargerFinger
    {
        get { return targetFinger; }
    }

    public GameObject HitObject
    {
        get { return hitObject; }
    }
}
