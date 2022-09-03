using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NumLock : InterectiveObject
{
    public static event Action Unlock = delegate{};
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Operate()
    {
        if (LockController.isNumLockOpen == true)
        {
            animator.SetBool("isActive", true);
            SoundManager.Instance.PlayObjectSound(audioSource, "RotateUnlock");
            StartCoroutine(DelayUnlock());
        }
    }

    float delayTime = 0.7f;
    private IEnumerator DelayUnlock()
    {
        yield return new WaitForSeconds(delayTime);
        Unlock();
    }
}
