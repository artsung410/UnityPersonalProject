using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemPickup : MonoBehaviour
{
    public static event Action<Item> PickUpSignal = delegate { };
    public Item Item;

    private void OnMouseDown()
    {
        StartCoroutine(DelayPickUp());
        Pickup();
    }

    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    IEnumerator DelayPickUp()
    {
        yield return new WaitForSeconds(0.4f);
        PickUpSignal(Item);
    }
}
