using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCameraController : MonoBehaviour
{
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        GestureManager.Instance.OnTwoFingerPan += OnTwoFingerPan;
    }

    private void OnDisable()
    {
        GestureManager.Instance.OnTwoFingerPan -= OnTwoFingerPan;
    }

    void OnTwoFingerPan(object sender, TwoFingerPanEventArgs args)
    {
        //Assign variables for easy access
        Vector2 delta1 = args.Finger1.deltaPosition;
        Vector2 delta2 = args.Finger2.deltaPosition;
        //Get the averafe between the two fingers
        Vector2 ave = new Vector2(
                                (delta1.x + delta2.x) / 2,
                                (delta1.y + delta2.y) / 2
                                 );
        //Since average is in pixels - convert it to a lower value
        ave = ave / Screen.dpi;
        //Compute the change in position
        Vector3 change = (Vector3)ave * speed;
        //Assign the position of the camera
        transform.position += change;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
