using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Operate()
    {
        if (true == PasswordController.IsGameWin)
        {
            isActive = true;
            animator.SetBool("isActive", true);
        }
    }
}
