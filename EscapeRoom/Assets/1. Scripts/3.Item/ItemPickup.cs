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
        SoundManager.Instance.playPickupSound(Item);
        Pickup();
    }

    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        PickUpSignal(Item);
        Debug.Log("DelayPickUp");
        Destroy(gameObject);
    }
}
