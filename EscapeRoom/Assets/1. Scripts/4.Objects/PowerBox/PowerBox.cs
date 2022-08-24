using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : InterectiveObject
{
    [Header("PowerBox")]
    [SerializeField] private GameObject Bolt;
    [SerializeField] private float activeTime_AfterCompletion;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Operate()
    {
        if (isOpened)
        {
            isActive = true;
            animator.SetBool("isActive", true);
            StartCoroutine(reset(activeTime_AfterCompletion));
        }

        else
        {
            Item item = InventoryManager.Instance.CurrentGripItem;

            if (item == null)
            {
                return;
            }

            if (item.itemName == NeedItemName)
            {
                isActive = true;
                animator.SetBool("isActive", true);
                Destroy(Bolt, 6f);
                StartCoroutine(reset(activeTime));
                StartCoroutine(Completion(activeTime));
            }
        }
    }
}
