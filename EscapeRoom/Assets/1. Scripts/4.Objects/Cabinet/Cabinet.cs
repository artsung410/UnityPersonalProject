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
        isActive = !isActive;
        animator.SetBool("isActive", isActive);
        if (true == isActive)
        {
            SoundManager.Instance.PlayObjectSound(audioSource, "IronDoorOpen", "IronDoorClose", isActive);
        }
        else
        {
            StartCoroutine(DelayPlaySound());
        }
    }

    IEnumerator DelayPlaySound()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayObjectSound(audioSource, "IronDoorOpen", "IronDoorClose", isActive);
    }
}
