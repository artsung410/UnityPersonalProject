using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : InterectiveObject
{
    [SerializeField] private GameObject Bolt;

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
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(Bolt, 6f);
            StartCoroutine(reset());
        }
    }
}
