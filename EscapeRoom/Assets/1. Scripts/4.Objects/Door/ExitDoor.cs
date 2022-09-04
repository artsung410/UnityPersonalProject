using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDoor : InterectiveObject
{
    bool isExitDoorOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Operate()
    {
        if (true == PasswordController.IsGameWin && false == isExitDoorOpen)
        {
            isActive = true;
            isExitDoorOpen = true;
            SoundManager.Instance.PlayObjectSound(audioSource, "ExitDoorOpen");
            animator.SetBool("isActive", true);
            PlayerHUD.Instance.EscapeSuccessUI.SetActive(true);
            GameManager.Instance.LoadScene((int)Scene.ending);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
