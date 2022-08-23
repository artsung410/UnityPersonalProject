using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Operate()
    {
        if (LockController.isNumLockOpen == true)
        {
            isActive = true;
            animator.SetBool("isActive", true);
            StartCoroutine(reset());
        }
    }
}
