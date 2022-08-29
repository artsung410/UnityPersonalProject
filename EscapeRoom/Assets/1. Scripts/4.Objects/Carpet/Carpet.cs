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
        isActive = !isActive;
        animator.SetBool("isActive", isActive);
        SoundManager.Instance.PlayObjectSound(audioSource, "Carpet");
    }
}
