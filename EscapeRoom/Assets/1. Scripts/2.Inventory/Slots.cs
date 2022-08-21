using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnCursorEnterEvent : UnityEngine.Events.UnityEvent<Sprite> { }
public class OnCursorExitEvent : UnityEngine.Events.UnityEvent { }

public class Slots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static OnCursorEnterEvent onCursorEnterEvent = new OnCursorEnterEvent();
    public static OnCursorExitEvent onCursorExitEvent = new OnCursorExitEvent();
    private Image ItemIcon;

    void Awake()
    {
        ItemIcon = GetComponent<Image>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Sprite ItemName = ItemIcon.sprite;
        onCursorEnterEvent.Invoke(ItemName);
        //Debug.Log(ItemName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onCursorExitEvent.Invoke();
        //Debug.Log(ItemName);
    }
}
