using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : InterectiveObject
{
    [Header("PowerBox")]
    [SerializeField] private GameObject Bolt;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Operate()
    {
        if (IsOpened)
        {
            // <여러번 들어오는 조건문>
            IsActive = !IsActive;
            animator.SetBool("isActive", IsActive);
            if (true == IsActive)
            {
                SoundManager.Instance.PlayObjectSound(audioSource, "IronDoorOpen", "IronDoorClose", IsActive);
            }
            else
            {
                StartCoroutine(delayPlaySound());
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
                Destroy(Bolt, 3.5f);
                StartCoroutine(delayLenchSound());
                StartCoroutine(setAvailability());
            }

            else
            {
                // 타 아이템들고, 마우스 클릭시
                SoundManager.Instance.PlayObjectSound(audioSource, "Locked");
            }
        }
    }

    private IEnumerator setAvailability()
    {
        yield return new WaitForSeconds(ActiveTime);
        IsOpened = true;
        animator.SetBool("isOpened", true);
        animator.SetBool("isActive", false);
    }

    float _delayTime = 0.6f;

    private IEnumerator delayLenchSound()
    {
        yield return new WaitForSeconds(_delayTime);
        SoundManager.Instance.PlayObjectSound(audioSource, "Lench");
    }

    private IEnumerator delayPlaySound()
    {
        yield return new WaitForSeconds(0.7f);
        SoundManager.Instance.PlayObjectSound(audioSource, "IronDoorOpen", "IronDoorClose", IsActive);
    }
}
