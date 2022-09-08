using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : InterectiveObject
{
    private bool isBookshelfMove;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PasswordController.PasswordUnlock += SetAvailability;
    }

    public override void Operate()
    {
        if (true == PasswordController.IsGameWin && false == isBookshelfMove)
        {
            IsActive = true;
            isBookshelfMove = true;
            SoundManager.Instance.PlayObjectSound(audioSource, "BoxMove");
            animator.SetBool("isActive", true);
        }
    }

    public void SetAvailability()
    {
        IsOpened = true;
    }
}
