using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapReceiver2 : MonoBehaviour, ITap
{
    public void OnTap()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
