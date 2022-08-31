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
        InitSetNumber(0);
    }

    public void InitSetNumber(int number)
    {
        numberShown = number;
    }

    private void OnMouseDown()
    {
        if (Enigma.Instance.IsZoomIn == true)
        {
            RotateWheel();
            animator.SetTrigger("Rotate");
        }
    }

    private void RotateWheel()
    {
        numberShown += 1;

        if (numberShown > 9)
        {
            numberShown = 0;
        }

        RotatedGear(name, numberShown);
        SoundManager.Instance.PlayObjectSound(audioSource, "RotateLock");
    }
}
