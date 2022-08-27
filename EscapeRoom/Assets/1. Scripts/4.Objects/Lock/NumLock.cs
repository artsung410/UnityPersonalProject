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
    }

    public override void Operate()
    {
        if (LockController.isNumLockOpen == true)
        {
            animator.SetBool("isActive", true);
            StartCoroutine(DelayUnlock());
        }
    }

    private IEnumerator DelayUnlock()
    {
        yield return new WaitForSeconds(0.7f);
        Unlock();
    }
}
