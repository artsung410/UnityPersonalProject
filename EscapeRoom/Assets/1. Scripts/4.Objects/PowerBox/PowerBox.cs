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
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void Operate()
    {
        if (isOpened)
        {
            // <������ ������ ���ǹ�>
            isActive = !isActive;
            animator.SetBool("isActive", isActive);
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
                isActive = true;
                animator.SetBool("isActive", true);
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
        isOpened = true;
        animator.SetBool("isOpened", true);
    }
}
