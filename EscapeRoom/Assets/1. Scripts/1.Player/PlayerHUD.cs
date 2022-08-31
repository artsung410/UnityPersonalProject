using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [Header("InventoryUI")]
    [SerializeField]  private GameObject      InventoryUI;

    [Header("ItemInfoUI")]
    [SerializeField]  private GameObject      ItemInfoPanelUI;
    [SerializeField]  private Image           ItemInfoImage;
    [SerializeField]  private TextMeshProUGUI ItemInfoTitleText;

    [Header("GetItemUI")]
    [SerializeField]  private GameObject      GetItemUI;
    [SerializeField]  private Image           GetItemImage;
    [HideInInspector] public bool             IsReadyToDeactiveGetItemUI;

    [Header("LockedUI")]
    [SerializeField]  private GameObject      LockedUI;

    [Header("MouseClickUI")]
    [SerializeField]  private GameObject      MouseClickUI;


    [Header("EnigmaUI")]
    [SerializeField]  private GameObject      EnigmaUI;


    void Start()
    {
        ItemPickup.PickUpSignal += ActiveGetItemUI;
        InterectiveObject.UnLockedMessage += ActiveLockedUI;
        InterectiveObject.SelectedMessage += ActiveMouseClickUI;
        Slots.onButtonClickEvent.AddListener(DeActiveInventoryUI);
        Slots.onCursorEnterEvent.AddListener(ActiveItemInfo);
        Slots.onButtonClickEvent.AddListener(DeActiveItemInfo);
    }

    // ItemInfo
    Item currnetItem;
    void ActiveItemInfo(Sprite itemImage)
    {
        // 아이템 사진
        foreach (Item item in InventoryManager.Instance.Items)
        {
            if (item.icon == itemImage)
            {
                currnetItem = item;
                ItemInfoTitleText.text = item.name.Replace("_", " ");
                ItemInfoImage.sprite = item.icon;
                ItemInfoPanelUI.SetActive(true);
                SoundManager.Instance.playItemInfoSound();
                break;
            }
        }

        // 아이템 타이틀
        // 아이템 설명
    }
    public void DeActiveItemInfo()
    {
        ItemInfoPanelUI.SetActive(false);
    }

    // EnigmaUI
    public void ActiveEnigmaUI()
    {
        EnigmaUI.SetActive(true);
    }

    public void DeActiveEnigmaUI()
    {
        EnigmaUI.SetActive(false);
    }

    // GetItemUI
    public bool IsActiveGetItemUI()
    {
        return GetItemUI.activeSelf;
    }

    public void ActiveGetItemUI(Item item)
    {
        GetItemUI.SetActive(true);
        GetItemImage.sprite = item.icon;
        StartCoroutine(DelayGetItemUISetBool());
    }

    public void DeActiveGetItemUI()
    {
        GetItemUI.SetActive(false);

        if (IsReadyToDeactiveGetItemUI == true)
        {
            IsReadyToDeactiveGetItemUI = false;
        }

    }
    IEnumerator DelayGetItemUISetBool()
    {
        yield return new WaitForSeconds(0.2f);
        IsReadyToDeactiveGetItemUI = true;
    }


    // InventoryUI
    public bool IsActiveInventoryUI()
    {
        return InventoryUI.activeSelf;
    }

    public void ActiveInventoryUI()
    {
        InventoryUI.SetActive(true);
    }
    public void DeActiveInventoryUI()
    {
        InventoryUI.SetActive(false);
        ItemInfoPanelUI.SetActive(false);
    }


    public void ActiveSelectedUI()
    {
        LockedUI.SetActive(false);
        MouseClickUI.SetActive(false);
    }

    // MouseClickUI
    public bool IsMouseClikedUI()
    {
        return MouseClickUI.activeSelf;
    }
    public void ActiveMouseClickUI()
    {
        LockedUI.SetActive(false);
        MouseClickUI.SetActive(true);
    }
    public void DeActiveMouseClickUI()
    {
        MouseClickUI.SetActive(false);
    }

    // LockedUI
    public bool IsActiveLockedUI()
    {
        return LockedUI.activeSelf;
    }

    public void ActiveLockedUI()
    {
        MouseClickUI.SetActive(false);
        LockedUI.SetActive(true);
    }

    public void DeActiveLockedUI()
    {
        LockedUI.SetActive(false);
    }
}
