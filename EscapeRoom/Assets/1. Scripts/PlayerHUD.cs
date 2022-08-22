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

    Item currnetItem;

    void Start()
    {
        Slots.onCursorEnterEvent.AddListener(ShowItemInfo);
        Slots.onCursorExitEvent.AddListener(CloseItemInfo);
        Slots.onButtonClickEvent.AddListener(CloseItemInfo);
    }

    void ShowItemInfo(Sprite itemImage)
    {
        // 아이템 사진
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

        // 아이템 타이틀

        // 아이템 설명
    }

    public void CloseItemInfo()
    {
        ItemInfoPanel.gameObject.SetActive(false);

    }
}
