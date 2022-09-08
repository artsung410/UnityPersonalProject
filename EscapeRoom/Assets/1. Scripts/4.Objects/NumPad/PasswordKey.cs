using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PasswordKey : MonoBehaviour
{
    public static event Action<int> KeypadSignal = delegate { };
    private             AudioSource audioSource;
    public int Key;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        StartCoroutine(pushed());
    }

    private IEnumerator pushed()
    {
        SoundManager.Instance.PlayObjectSound(audioSource, "PushKey");
        KeypadSignal(Key);
        yield return null;
    }
}

