using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetNeedsKey : InterectiveObject
{
    [Header("CabinetNeedsKey")]
    [SerializeField] private GameObject Lock;
    [SerializeField] private float activeTime_AfterCompletion;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Operate()
    {
        if(isOpened)
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
                gameObject.GetComponent<MeshCollider>().enabled = false;
                Destroy(Lock, 6f);
                StartCoroutine(reset(activeTime));
                StartCoroutine(Completion(activeTime));
            }
        }
    }
}
