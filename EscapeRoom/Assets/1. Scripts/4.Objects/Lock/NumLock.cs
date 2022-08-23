using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumLock : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Operate()
    {
        if (LockController.isNumLockOpen == true)
        {
            animator.SetBool("isActive", true);
        }
    }
}
