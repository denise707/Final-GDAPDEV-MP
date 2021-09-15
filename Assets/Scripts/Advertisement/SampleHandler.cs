using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleHandler : MonoBehaviour
{
    AdsManager adsManager;

    // Start is called before the first frame update
    void Start()
    {
        adsManager.OnAdDone += OnAdDone;
    }

    private void OnAdDone(object sender, AdFinishEventArgs e)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
