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
    [SerializeField]  private GameObject      ReturnButtonUI;
    [SerializeField]  private GameObject      EnigmaInitButtonUI;

    void Start()
    {
        ItemPickup.PickUpSignal += ActiveGetItemUI;
        InterectiveObject.UnLockedMessage += ActiveLockedUI;
        InterectiveObject.SelectedMessage += ActiveMouseClickUI;
        EnigmaCollider.colliderClickSignal += ActiveEnigmaInitButtonUI;

        Slots.onCursorEnterEvent += ActiveItemInfo;
        Slots.onButtonClickEvent += DeActiveInventoryUI;
        Slots.onButtonClickEvent += DeActiveItemInfo;
    }

    // ItemInfo
    Item currnetItem;
    void ActiveItemInfo(Sprite itemImage)
    {
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
    }
    public void DeActiveItemInfo()
    {
        ItemInfoPanelUI.SetActive(false);
    }

    // EnigmaUI
    public void DeActiveEnigmaSceneUI()
    {
        DeActiveReturnButtonUI();
        DeActiveEnigmaInitButtonUI();
    }

    public bool IsActiveReturnButtonUI()
    {
        return ReturnButtonUI.activeSelf;
    }

    public void ActiveReturnButtonUI()
    {
        ReturnButtonUI.SetActive(true);
    }

    public void DeActiveReturnButtonUI()
    {
        ReturnButtonUI.SetActive(false);
    }


    public bool IsActiveEnigmaInitButtonUI()
    {
        return EnigmaInitButtonUI.activeSelf;
    }

    public void ActiveEnigmaInitButtonUI()
    {
        EnigmaInitButtonUI.SetActive(true);
    }

    public void DeActiveEnigmaInitButtonUI()
    {
        EnigmaInitButtonUI.SetActive(false);
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
