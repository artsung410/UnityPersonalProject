using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum ItemTag
{
    Lench,
    Note,
    Silver_Key,
    Gold_Key,
    Iron_Cross,
    Enigma_Battery,
    Enigma_Gear,
    Enimga_Keyboard,
    Enigma_Box,
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        //Clean content before open.
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.sprite = item.icon;
        }
    }

    //public void EnableItemsRemove()
    //{
    //    if (EnableRemove.isOn)
    //    {
    //        foreach (Transform item in ItemContent)
    //        {
    //            item.Find("RemoveButton").gameObject.SetActive(true);
    //        }
    //    }
    //    else
    //    {
    //        foreach (Transform item in ItemContent)
    //        {
    //            item.Find("RemoveButton").gameObject.SetActive(false);
    //        }
    //    }
    //}

    //public void SetInventoryItems()
    //{
    //    InventoryItems = ItemContent.GetComponentInChildren<InventoryItemController>();

    //    for (int)
    //}
}
