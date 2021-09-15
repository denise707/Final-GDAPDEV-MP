using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeReceiver : MonoBehaviour, ISwipped, IDragged, ISpread, IRotate
{
    public float speed = 10;
    private Vector3 TargetPos = Vector3.zero;
    public float resizeSpeed = 5;
    public float rotateSpeed = 1;

    public void OnDrag(DragEventArgs args)
    {
        Ray r = Camera.main.ScreenPointToRay(args.TargerFinger.position);
        Vector3 worldPoint = r.GetPoint(10);

        TargetPos = worldPoint;
        transform.position = worldPoint;
    }

    public void OnSpread(SpreadEventArgs args)
    {
        float scale = (args.DistanceDelta / Screen.dpi) * resizeSpeed;
        Vector3 scaleVector = new Vector3(scale, scale, scale);
        transform.localScale += scaleVector;
    }

    public void OnSwipe(SwipeEventArgs args)
    {
        Vector3 dir = (Vector3)args.SwipeVector.normalized;

        TargetPos += (dir * 5);
    }

    // Start is called before the first frame update
    private void onEnable()
    {
        TargetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);
    }

    public void OnRotate(RotateEventArgs args)
    {
        float angle = args.Angle * rotateSpeed;
        if(args.RotationDirection == RotationDirections.CW)
        {
            //Revers the rotation on CW
            angle *= -1;
        }

        transform.Rotate(0, 0, angle);
    }
}
