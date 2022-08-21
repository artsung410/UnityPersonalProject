using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject ItemInfoPanel;
    [SerializeField] private Image ItemInfoImage;
    [SerializeField] private TextMeshProUGUI ItemInfoTitleText;
    [SerializeField] private TextMeshProUGUI ItemInfoDescriptionText;


    Item currnetItem;


    void Start()
    {
        Slots.onCursorEnterEvent.AddListener(ShowItemInfo);
        Slots.onCursorExitEvent.AddListener(CloseItemInfo);
    }

    void ShowItemInfo(Sprite itemImage)
    {
        // ������ ����
        foreach (Item item in InventoryManager.Instance.Items)
        {
            if (item.icon == itemImage)
            {
                currnetItem = item;
                ItemInfoTitleText.text = item.name;
                ItemInfoImage.sprite = item.icon;
                ItemInfoPanel.SetActive(true);
                break;
            }
        }

        // ������ Ÿ��Ʋ

        // ������ ����
    }

    void CloseItemInfo()
    {
        if (currnetItem != null)
        {
            ItemInfoPanel.gameObject.SetActive(false);
        }
    }
}
