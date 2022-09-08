using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Operate()
    {
        IsActive = !IsActive;
        animator.SetBool("isActive", IsActive);
        if (true == IsActive)
        {
            SoundManager.Instance.PlayObjectSound(audioSource, "IronDoorOpen", "IronDoorClose", IsActive);
        }
        else
        {
            StartCoroutine(DelayPlaySound());
        }
    }

    IEnumerator DelayPlaySound()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayObjectSound(audioSource, "IronDoorOpen", "IronDoorClose", IsActive);
    }
}
