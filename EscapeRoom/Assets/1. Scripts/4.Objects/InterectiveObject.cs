using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class InterectiveObject : MonoBehaviour
{
    public static event Action UnLockedMessage = delegate { };
    public static event Action SelectedMessage = delegate { };

    [Header("InterectiveObject")]
    public float activeTime;
    public string NeedItemName;

    public bool isActive;
    [HideInInspector] public bool isOpened;
    protected Animator animator;
    protected AudioSource audioSource;

    public abstract void Operate();

    public void PopUpMessage()
    {
        if (NeedItemName.Length >= 1 && false == isOpened)
        {
            UnLockedMessage();
        }
        else
        {
            SelectedMessage();
        }
    }
}
