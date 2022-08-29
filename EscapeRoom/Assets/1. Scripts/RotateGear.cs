using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RotateGear : MonoBehaviour
{
    public static event Action<string, int> RotatedGear = delegate { };
    private Animator animator;
    private AudioSource audioSource;
    private int numberShown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        numberShown = 0;
    }

    private void OnMouseDown()
    {
        if (Enigma.Instance.IsZoomIn == true)
        {
            animator.SetTrigger("Rotate");
            StartCoroutine("RotateWheel");
        }
    }

    private IEnumerator RotateWheel()
    {
        numberShown += 1;

        if (numberShown > 9)
        {
            numberShown = 0;
        }

        RotatedGear(name, numberShown);
        SoundManager.Instance.PlayObjectSound(audioSource, "RotateLock");
        yield return null;
    }
}
