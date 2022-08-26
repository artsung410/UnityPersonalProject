using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EnigmaCollider : MonoBehaviour
{
    public static event Action<int> transmitID = delegate { };

    [SerializeField] private int id;
    private BoxCollider boxCol;

    private void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
        transmitID(id);
        boxCol.enabled = false;
        Enigma.Instance.IsZoomIn = true;
    }

    public void ActiveCollider()
    {
        boxCol.enabled = true;
        Enigma.Instance.IsZoomIn = false;
    }
}
