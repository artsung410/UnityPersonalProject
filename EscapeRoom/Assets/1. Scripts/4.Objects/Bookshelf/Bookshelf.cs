using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : InterectiveObject
{
    bool isBookshelfMove;

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
            isActive = true;
            isBookshelfMove = true;
            SoundManager.Instance.PlayObjectSound(audioSource, "BoxMove");
            animator.SetBool("isActive", true);
        }
    }

    public void SetAvailability()
    {
        isOpened = true;
    }
}
