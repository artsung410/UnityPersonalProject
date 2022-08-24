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
        Debug.Log("�ݶ��̴� ����");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("�ݶ��̴� ���");
    }

    private void OnMouseDown()
    {
        transmitID(id);
    }
}
