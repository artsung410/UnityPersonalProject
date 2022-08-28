using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Operate()
    {
        isActive = true;
        animator.SetBool("isActive", true);
        SoundManager.Instance.PlayObjectSound(audioSource, "BoxMove");
        StartCoroutine(reset(activeTime));
        StartCoroutine(PlayExitSound(activeTime, "BoxReset"));
    }
}
