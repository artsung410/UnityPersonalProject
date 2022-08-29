using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : InterectiveObject
{
    bool isExitDoorOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void Operate()
    {
        if (true == PasswordController.IsGameWin && false == isExitDoorOpen)
        {
            isActive = true;
            isExitDoorOpen = true;
            SoundManager.Instance.PlayObjectSound(audioSource, "ExitDoorOpen");
            animator.SetBool("isActive", true);
        }
    }
}
