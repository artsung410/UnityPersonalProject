using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetNeedsKey : InterectiveObject
{
    [SerializeField] private GameObject Lock;
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
            gameObject.GetComponent<MeshCollider>().enabled = false;
            Destroy(Lock, 6f);
            StartCoroutine(reset());
        }
    }
}
