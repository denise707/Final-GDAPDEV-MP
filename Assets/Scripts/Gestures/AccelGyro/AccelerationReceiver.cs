using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationReceiver : MonoBehaviour
{
    public float speed = 5;
    //Min change in x before we start moving
    public float minChange = 0.2f;

    private void FixedUpdate()
    {
        //Gets how much the phone is tilted in the X axis
        float x = Input.acceleration.x;
        if(Mathf.Abs(x) >= minChange)
        {
            x *= speed * Time.deltaTime;
            //Moves character by how much phone is tilted
            transform.Translate(x, 0, 0);
        }     
    }

    private void OnDrawGizmos()
    {
        //Only draw ray when game is running
        if (Application.isPlaying)
        {
            //Draw red from origin to same dir as the acceleroneter values
            Debug.DrawRay(transform.position, Input.acceleration.normalized, Color.red);
        }
    }
}
