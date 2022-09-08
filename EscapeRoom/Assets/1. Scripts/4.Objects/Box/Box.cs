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
        IsActive = !IsActive;
        animator.SetBool("isActive", IsActive);
        SoundManager.Instance.PlayObjectSound(audioSource, "BoxMove", "BoxReset", IsActive);
    }
}
