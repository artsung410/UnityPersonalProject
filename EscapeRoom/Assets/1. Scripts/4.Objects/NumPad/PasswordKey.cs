using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PasswordKey : MonoBehaviour
{
    public static event Action<int> KeypadSignal = delegate { };
    private             AudioSource audioSource;
    private int _Key;
    public  int  Key { get { return _Key; } set { _Key = value; }}


    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        StartCoroutine("Pushed");
    }

    private IEnumerator pushed()
    {
        SoundManager.Instance.PlayObjectSound(audioSource, "PushKey");
        KeypadSignal(Key);
        yield return null;
    }
}

