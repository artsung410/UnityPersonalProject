using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : InterectiveObject
{
    [Header("ToolBox")]
    [SerializeField] private float activeTime_AfterCompletion;
    private bool isPickUpKey;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        NumLock.Unlock += SetAvailability;
    }

    public override void Operate()
    {
        if (LockController.isNumLockOpen == true)
        {
            isActive = !isActive;
            animator.SetBool("isActive", isActive);
            SoundManager.Instance.PlayObjectSound(audioSource, "ToolBoxOpen", "ToolBoxClose", isActive);
        }

        else
        {
            SoundManager.Instance.PlayObjectSound(audioSource, "Locked");
        }
    }

    public void SetAvailability()
    {
        isOpened = true;
    }
}
