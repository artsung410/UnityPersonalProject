using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PasswordKey : MonoBehaviour
{
    public int Key;

    public static event Action<int> KeypadSignal = delegate { };
    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        StartCoroutine("Pushed");
    }

    private void Pushed()
    {
        KeypadSignal(Key);
    }
}

