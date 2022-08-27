using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PasswordController.PasswordUnlock += SetAvailability;
    }

    public override void Operate()
    {
        if (true == PasswordController.IsGameWin)
        {
            isActive = true;
            animator.SetBool("isActive", true);
        }
    }

    public void SetAvailability()
    {
        isOpened = true;
    }
}
