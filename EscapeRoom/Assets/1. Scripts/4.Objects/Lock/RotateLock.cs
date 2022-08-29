using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateLock : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate { };

    private int numberShown;
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
        StartCoroutine("RotateWheel");
    }

    float angle;
    float rotorSpeed = 0.01f;
    float degree = 1f;

    private IEnumerator RotateWheel()
    {
        SoundManager.Instance.PlayObjectSound(audioSource, "RotateLock");

        angle = 36f;

        for (int i = 0; i < angle; i++)
        {
            yield return new WaitForSeconds(rotorSpeed);
            transform.Rotate(0f, 0f, degree);
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
