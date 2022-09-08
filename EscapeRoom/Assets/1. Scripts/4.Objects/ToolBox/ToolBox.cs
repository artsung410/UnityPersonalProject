using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : InterectiveObject
{
    [Header("ToolBox")]
    [SerializeField] private float activeTime_AfterCompletion;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        NumLock.Unlock += SetAvailability;
    }

    public override void Operate()
    {
        if (LockController.isNumLockOpen == true)
        {
            IsActive = !IsActive;
            animator.SetBool("isActive", IsActive);
            if (true == IsActive)
            {
                SoundManager.Instance.PlayObjectSound(audioSource, "ToolBoxOpen", "ToolBoxClose", IsActive);
            }
            else
            {
                StartCoroutine(delayPlaySound());
            }

        }

        else
        {
            SoundManager.Instance.PlayObjectSound(audioSource, "Locked");
        }
    }

    public void SetAvailability()
    {
        IsOpened = true;
    }

    private IEnumerator delayPlaySound()
    {
        yield return new WaitForSeconds(1.5f);
        SoundManager.Instance.PlayObjectSound(audioSource, "ToolBoxOpen", "ToolBoxClose", IsActive);
    }
}
