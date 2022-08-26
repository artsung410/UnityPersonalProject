using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    Item currnetItem;
    [Header("GetItemUI")]
    public GameObject GetItemUI;
    [SerializeField] private Image GetItemImage;

    [Header("InventoryUI")]
    public GameObject InventoryUI;

    [Header("ItemInfoUI")]
    [SerializeField] private GameObject ItemInfoPanelUI;
    [SerializeField] private Image ItemInfoImage;
    [SerializeField] private TextMeshProUGUI ItemInfoTitleText;

    [Header("PickUpUI")]
    public GameObject PickUpUI;

    [Header("EnigmaUI")]
    [SerializeField] private GameObject EnigmaUI;



    void Start()
    {
        Slots.onButtonClickEvent.AddListener(DeActiveInventory);
        Slots.onCursorEnterEvent.AddListener(ActiveItemInfo);
        Slots.onButtonClickEvent.AddListener(DeActiveItemInfo);
    }

    // ItemInfo
    void ActiveItemInfo(Sprite itemImage)
    {
        // 아이템 사진
        foreach (Item item in InventoryManager.Instance.Items)
        {
            if (item.icon == itemImage)
            {
                currnetItem = item;
                ItemInfoTitleText.text = item.name;
                ItemInfoImage.sprite = item.icon;
                ItemInfoPanelUI.SetActive(true);
                break;
            }
        }

        // 아이템 타이틀
        // 아이템 설명
    }

    public void ActiveEnigmaUI()
    {
        EnigmaUI.SetActive(true);
    }

    public void ActiveGetItemUI(Item item)
    {
        GetItemUI.SetActive(true);
        GetItemImage.sprite = item.icon;
    }

    public void DeActiveItemInfo()
    {
        ItemInfoPanelUI.SetActive(false);
    }

    // Inventory
    public void DeActiveInventory()
    {
        InventoryUI.SetActive(false);
        ItemInfoPanelUI.SetActive(false);
    }

    public void DeActivePickUpUI()
    {
        PickUpUI.SetActive(false);
    }
}
