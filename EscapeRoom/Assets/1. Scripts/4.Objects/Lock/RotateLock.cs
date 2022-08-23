using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RotateLock : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate { };

    private int numberShown;

    private void Start()
    {
        numberShown = 3;
    }

    private void OnMouseDown()
    {
        StartCoroutine("RotateWheel");

    }

    float angle;
    private IEnumerator RotateWheel()
    {
        angle = 36f;
        transform.Rotate(0f, 0f, angle);

        numberShown += 1;

        if (numberShown > 9)
        {
            numberShown = 0;
        }

        Rotated(name, numberShown); // Event Invkoe ¿ªÇÒÇÔ.

        yield return null;
    }
}
