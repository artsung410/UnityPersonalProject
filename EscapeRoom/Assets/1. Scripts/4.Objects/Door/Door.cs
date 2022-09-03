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
        isActive = !isActive;
        animator.SetBool("isActive", isActive);
        if (true == isActive)
        {
            SoundManager.Instance.PlayObjectSound(audioSource, "WoddenDoorOpen", "WoddenDoorClose", isActive);
        }
        else
        {
            StartCoroutine(DelayPlaySound());
        }
    }

    IEnumerator DelayPlaySound()
    {
        yield return new WaitForSeconds(0.7f);
        SoundManager.Instance.PlayObjectSound(audioSource, "WoddenDoorOpen", "WoddenDoorClose", isActive);
    }
}
