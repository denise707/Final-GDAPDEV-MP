using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogError("No gyro in device");
        }
    }

    // Camera Gyro
    void FixedUpdate()
    {
        if (Input.gyro.enabled)
        {
            Vector3 rotation = Input.gyro.rotationRate;

            //Invert x and y
            rotation.x *= -1;
            rotation.y *= -1;

            transform.Rotate(rotation);
        }
    }

    //private void FixedUpdate()
    //{
    //    Quaternion rot = GyroToUnity(Input.gyro.attitude);
    //    transform.rotation = rot;
    //}

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
