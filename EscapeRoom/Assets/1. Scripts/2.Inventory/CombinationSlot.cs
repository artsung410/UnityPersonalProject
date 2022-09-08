using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CombinationSlot : MonoBehaviour
{
    public static event Action<Item> CombinationComplete = delegate { };
  
    [SerializeField] private List<Item> CslotItems = new List<Item>();
    [SerializeField] private Item       EnigmaItem;
    private          int                _ChildSpriteMaxCount = 4;
    private          int                _EnigmaToolsCount = 0;

    void Start()
    {
        Slots.onTransmtItem += InputItem;
        Cslots.CslotsButtonClickSignal += RemoveItem;
    }

    private void remveAllSprite()
    {
        for (int i = 0; i < _ChildSpriteMaxCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        }
    }

    private void listedSlots()
    {
        for (int i = 0; i < CslotItems.Count; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            transform.GetChild(i).gameObject.GetComponent<Image>().sprite = CslotItems[i].icon;
        }
    }

    public void RunCombination()
    {
        CombinationComplete.Invoke(EnigmaItem);
    }

    public void InitSlots()
    {
        if (CslotItems.Count == 4 && _EnigmaToolsCount == 4)
        {
            RunCombination();
            Debug.Log("조합 완료");
        }

        for (int i = CslotItems.Count - 1; i >= 0; i--)
        {
            transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
            transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
            CslotItems.Remove(CslotItems[i]);
        }

        _EnigmaToolsCount = 0;
    }

    public void InputItem(Item item)
    {
        for (int i = 0; i < CslotItems.Count; i++)
        {
            if (CslotItems[i].icon == item.icon)
            {
                return;
            }
        }

        if (CslotItems.Count == 4)
        {
            return;
        }

        CslotItems.Add(item);
        listedSlots();

        if (item.id == 5 || item.id == 6 || item.id == 7 || item.id == 8)
        {
            ++_EnigmaToolsCount;
        }
    }

    public void RemoveItem(Sprite sprite)
    {
        for (int i = 0; i < CslotItems.Count; i++)
        {
            if (CslotItems[i].itemName == sprite.name)
            {
                CslotItems.Remove(CslotItems[i]);
                transform.GetChild(i).gameObject.GetComponent<Image>().sprite = null;
                transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
            }
        }

        remveAllSprite();
        listedSlots();
    }


}
