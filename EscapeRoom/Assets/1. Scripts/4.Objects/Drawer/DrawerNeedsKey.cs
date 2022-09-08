using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerNeedsKey : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Operate()
    {
        if (IsOpened)
        {
            // <������ ������ ���ǹ�>
            IsActive = !IsActive;
            animator.SetBool("isActive", IsActive);
            SoundManager.Instance.PlayObjectSound(audioSource, "DrawerOpen", "DrawerClose", IsActive);
        }

        else
        {
            // <�ѹ��� ������ ���ǹ�>
            Item item = InventoryManager.Instance.CurrentGripItem;

            if (item == null)
            {
                // ������ �ȵ���ְ�, ���콺 Ŭ����
                SoundManager.Instance.PlayObjectSound(audioSource, "Locked");
                return;
            }

            if (item.itemName == NeedItemName)
            {
                // �ùٸ� ������ ���, ���콺 Ŭ����
                animator.SetBool("isActive", true);
                StartCoroutine(PlayKeyUnlockSound());
                StartCoroutine(SetAvailability());
            }

            else
            {
                // Ÿ �����۵��, ���콺 Ŭ����
                SoundManager.Instance.PlayObjectSound(audioSource, "Locked");
            }
        }
    }

    private IEnumerator SetAvailability()
    {
        yield return new WaitForSeconds(ActiveTime);
        IsOpened = true;
        animator.SetBool("isOpened", true);
        animator.SetBool("isActive", false);
    }

    float _delayTime = 1.3f;
    private IEnumerator PlayKeyUnlockSound()
    {
        yield return new WaitForSeconds(_delayTime);
        SoundManager.Instance.PlayObjectSound(audioSource, "Unlock");
    }
}
