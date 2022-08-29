using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetNeedsKey : InterectiveObject
{
    [Header("CabinetNeedsKey")]
    [SerializeField] private GameObject Lock;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void Operate()
    {
        if(isOpened)
        {
            isActive = !isActive;
            animator.SetBool("isActive", isActive);
            SoundManager.Instance.PlayObjectSound(audioSource, "IronDoorOpen", "IronDoorClose", isActive);
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
        yield return new WaitForSeconds(activeTime);
        Destroy(Lock);
        isOpened = true;
        animator.SetBool("isOpened", true);
        animator.SetBool("isActive", false);
    }


    float delayTime = 2.2f;
    private IEnumerator PlayKeyUnlockSound()
    {
        yield return new WaitForSeconds(delayTime);
        SoundManager.Instance.PlayObjectSound(audioSource, "Unlock");
    }
}
