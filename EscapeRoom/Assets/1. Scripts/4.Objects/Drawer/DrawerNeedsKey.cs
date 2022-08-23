using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerNeedsKey : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Operate()
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
            StartCoroutine(reset());
        }
    }
}
