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
        SoundManager.Instance.PlayObjectSound(audioSource, "BoxMove", "BoxReset", isActive);
        isActive = !isActive;
        animator.SetBool("isActive", isActive);
    }
}
