using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateLock : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate { };

    private int         numberShown;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        numberShown = 3;
    }

    private void OnMouseDown()
    {
        if(false == LockController.isNumLockOpen)
        {
            StartCoroutine(rotateWheel());
        }
        else
        {
            StopCoroutine(rotateWheel());
        }
    }

    private float _angle;
    private float _rotorSpeed = 0.01f;
    private float _degree = 1f;

    private IEnumerator rotateWheel()
    {
        SoundManager.Instance.PlayObjectSound(audioSource, "RotateLock");

        _angle = 36f;

        for (int i = 0; i < _angle; i++)
        {
            yield return new WaitForSeconds(_rotorSpeed);
            transform.Rotate(0f, 0f, _degree);
        }

        numberShown += 1;

        if (numberShown > 9)
        {
            numberShown = 0;
        }

        Rotated(name, numberShown); // Event Invkoe ¿ªÇÒÇÔ.

        yield return null;
    }
}
