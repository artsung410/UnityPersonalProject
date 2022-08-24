using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EnigmaCollider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<int> transmitID = delegate { };

    [SerializeField] private int id;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("콜라이더 감지");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("콜라이더 벗어남");
    }

    private void OnMouseDown()
    {
        transmitID(id);
    }
}
