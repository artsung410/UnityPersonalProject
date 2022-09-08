using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void Operate()
    {
        IsActive = !IsActive;
        animator.SetBool("isActive", IsActive);
        if (true == IsActive)
        {
            SoundManager.Instance.PlayObjectSound(audioSource, "WoddenDoorOpen", "WoddenDoorClose", IsActive);
        }
        else
        {
            StartCoroutine(DelayPlaySound());
        }
    }

    IEnumerator DelayPlaySound()
    {
        yield return new WaitForSeconds(0.7f);
        SoundManager.Instance.PlayObjectSound(audioSource, "WoddenDoorOpen", "WoddenDoorClose", IsActive);
    }
}
