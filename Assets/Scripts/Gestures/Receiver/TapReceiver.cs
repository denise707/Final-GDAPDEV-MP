using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapReceiver : MonoBehaviour
{
    public GameObject spawn;
    //private GestureManager OnTap;

    public void Spawn(Vector3 pos)
    {
        Instantiate(spawn, pos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Register our tap function
        GestureManager.Instance.OnTap += OnTap;
    }


    private void OnTap(object send, TapEventArgs args)
    {
        //Spawn if theres nothing blocking the tap
        if (args.TapObject == null)
        {
            Ray r = Camera.main.ScreenPointToRay(args.TapPosition);
            Spawn(r.GetPoint(10));
        }
        else
        {
            Debug.Log($"Hit {args.TapObject.name}");
        }
    }   

    private void OnDisable()
    {
        //Deregister our tap function for cleanup
        GestureManager.Instance.OnTap -= OnTap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
