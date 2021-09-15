using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnScreenParent : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image hitbox;
    public OnScreenJoystick joystick;

    public void OnDrag(PointerEventData eventData)
    {
        if (Options.aim_mode)
        {
            joystick.OnDrag(eventData);
        }
            
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Options.aim_mode)
        {
            Vector2 localPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(hitbox.rectTransform, eventData.position, eventData.pressEventCamera, out localPos))
            {
                joystick.gameObject.SetActive(true);
                RectTransform rTrans = joystick.gameObject.GetComponent<RectTransform>();
                rTrans.localPosition = localPos;

                joystick.OnPointerDown(eventData);
            }
        }   
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Options.aim_mode)
        {
            joystick.gameObject.SetActive(false);
            joystick.OnPointerUp(eventData);
        }     
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
