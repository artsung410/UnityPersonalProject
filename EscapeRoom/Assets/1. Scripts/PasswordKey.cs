using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PasswordKey : MonoBehaviour
{
    public int Key;

    public static event Action<int> KeypadSignal = delegate { };
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        StartCoroutine("Pushed");
    }

    IEnumerator Pushed()
    {
        SoundManager.Instance.PlayObjectSound(audioSource, "PushKey");
        KeypadSignal(Key);
        yield return null;
    }
}

