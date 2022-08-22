using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    Item currnetItem;

    [Header("Inventory")]
    public GameObject InventoryUI;
    public bool isActiveInventory;

    [Header("ItemInfo")]
    [SerializeField] private GameObject ItemInfoPanelUI;
    [SerializeField] private Image ItemInfoImage;
    [SerializeField] private TextMeshProUGUI ItemInfoTitleText;

    void Start()
    {
        Slots.onButtonClickEvent.AddListener(DeActiveInventory);
        Slots.onCursorEnterEvent.AddListener(ActiveItemInfo);
        Slots.onButtonClickEvent.AddListener(DeActiveItemInfo);
    }

    // ItemInfo
    void ActiveItemInfo(Sprite itemImage)
    {
        // ������ ����
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

        // ������ Ÿ��Ʋ
        // ������ ����
    }

    public void DeActiveItemInfo()
    {
        ItemInfoPanelUI.gameObject.SetActive(false);
    }


    // Inventory
    public void DeActiveInventory()
    {
        isActiveInventory = false;
        InventoryUI.SetActive(isActiveInventory);
    }
}
