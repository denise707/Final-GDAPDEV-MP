using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnScreenJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image JoystickParent;
    [SerializeField] Image Stick;

    //Current Input Input.GetAxis("Horizontal")
    public Vector2 JoystickAxis = Vector2.zero;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPos; //Local position of touch
        //To get this, its just like physics raycast but in UI
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickParent.rectTransform, eventData.position, eventData.pressEventCamera, out localPos)) //Gets local position when you are touching jostick parent
        {
            float half_w = JoystickParent.rectTransform.rect.width / 2;
            float half_h = JoystickParent.rectTransform.rect.height / 2;

            float x = localPos.x / half_w;
            float y = localPos.y / half_h;

            //-1 -> +1 (must be contained here)
            JoystickAxis.x = x;
            JoystickAxis.y = y;

            if (JoystickAxis.magnitude > 1) { JoystickAxis.Normalize(); }

            this.Stick.rectTransform.localPosition = new Vector2(JoystickAxis.x * half_w, JoystickAxis.y * half_h);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        JoystickAxis = Vector2.zero;
        Stick.rectTransform.localPosition = Vector2.zero;
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
