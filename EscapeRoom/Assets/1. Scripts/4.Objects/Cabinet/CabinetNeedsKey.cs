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
        audioSource = GetComponent<AudioSource>();
    }

    public override void Operate()
    {
        if(IsOpened)
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
        else
        {
            // <한번만 들어오는 조건문>
            
            Item item = InventoryManager.Instance.CurrentGripItem;

            if (item == null)
            {
                // 아이템 안들고있고, 마우스 클릭시
                SoundManager.Instance.PlayObjectSound(audioSource, "Locked");
                return;
            }

            if (item.itemName == NeedItemName)
            {
                // 올바른 아이템 들고, 마우스 클릭시
                animator.SetBool("isActive", true);
                StartCoroutine(PlayKeyUnlockSound());
                StartCoroutine(SetAvailability());
            }

            else
            {
                // 타 아이템들고, 마우스 클릭시
                SoundManager.Instance.PlayObjectSound(audioSource, "Locked");
            }
        }
    }

    private IEnumerator SetAvailability()
    {
        yield return new WaitForSeconds(ActiveTime);
        Destroy(Lock);
        IsOpened = true;
        animator.SetBool("isOpened", true);
        animator.SetBool("isActive", false);
    }


    private float _delayTime = 2.2f;
    private IEnumerator PlayKeyUnlockSound()
    {
        yield return new WaitForSeconds(_delayTime);
        SoundManager.Instance.PlayObjectSound(audioSource, "Unlock");
    }

    IEnumerator DelayPlaySound()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayObjectSound(audioSource, "IronDoorOpen", "IronDoorClose", IsActive);
    }
}
