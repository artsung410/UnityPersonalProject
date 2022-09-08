using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : InterectiveObject
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
        SoundManager.Instance.PlayObjectSound(audioSource, "Carpet");
    }
}
