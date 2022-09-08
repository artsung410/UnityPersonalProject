using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class InterectiveObject : MonoBehaviour
{
    public static event Action UnLockedMessage = delegate { };
    public static event Action SelectedMessage = delegate { };

    [Header("InterectiveObject")]
    private float _activeTime;
    private string _needItemName;
    private bool _isActive;
    private bool _isOpend;

    protected Animator animator;
    protected AudioSource audioSource;
    public string NeedItemName;
    public float ActiveTime;

    public bool IsActive
    {
        get
        {
            return _isActive;
        }

        set
        {
            _isActive = value;
        }
    }

    public bool IsOpened
    {
        get
        {
            return _isOpend;
        }

        set
        {
            _isOpend = value;
        }
    }
   
    public abstract void Operate();

    public void PopUpMessage()
    {
        if (NeedItemName.Length >= 1 && false == IsOpened)
        {
            UnLockedMessage();
        }
        else
        {
            SelectedMessage();
        }
    }
}
