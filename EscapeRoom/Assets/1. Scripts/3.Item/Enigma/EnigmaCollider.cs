using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EnigmaCollider : MonoBehaviour
{
    public static event Action<int> transmitID = delegate { };
    public static event Action colliderClickSignal = delegate { };

    [SerializeField] private int id;
    private BoxCollider boxCol;

    private void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
        transmitID(id);
        colliderClickSignal();
        PlayClickSound();
        boxCol.enabled = false;
        Enigma.Instance.IsZoomIn = true;
    }

    public void ActiveCollider()
    {
        boxCol.enabled = true;
        Enigma.Instance.IsZoomIn = false;
    }

    private void PlayClickSound()
    {
        SoundManager.Instance.PlayEnigmaSwitchingSound();
    }
}
