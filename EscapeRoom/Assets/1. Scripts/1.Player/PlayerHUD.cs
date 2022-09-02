using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public static PlayerHUD Instance;

    [Header("InventoryUI")]
    [SerializeField]  private GameObject      centerDot;

    [Header("InventoryUI")]
    [SerializeField]  private GameObject      InventoryUI;

    [Header("CombinationUI")]
    [SerializeField]  private GameObject      CombinationUI;

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
    [SerializeField]  private GameObject      EnigmaInitButtonUI;

    [Header("ESC_UI")]
    [SerializeField]  private GameObject      ESC_UI;

    [Header("EscapeSuccessUI")]
    public GameObject      EscapeSuccessUI;

    [Header("FadeInOutImage")]
    public GameObject      FadeImage;

    [Header("HintImage")]
    public GameObject      HintImage;
    public Sprite[]        HintSprites;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(StartFadeIn());
        CombinationSlot.CombinationComplete += ActiveGetItemUI;
        ItemPickup.PickUpSignal += ActiveGetItemUI;
        InterectiveObject.UnLockedMessage += ActiveLockedUI;
        InterectiveObject.SelectedMessage += ActiveMouseClickUI;
        EnigmaCollider.colliderClickSignal += ActiveEnigmaInitButtonUI;

        Slots.onCursorEnterEvent += ActiveItemInfo;
        Slots.onButtonClickEvent += DeActive_UI_Main_All;
    }

    IEnumerator StartFadeIn()
    {
        float fadeCount = 1;

        Image image = FadeImage.GetComponent<Image>();

        while (fadeCount > 0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }

        FadeImage.SetActive(false);
    }

    // 메인씬에 있는 UI 전부 비활성화
    public void DeActive_UI_Main_All()
    {
        DeActiveInventoryUI();
        DeActiveItemInfo();
        DeActiveESC_UI();
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
        ActiveESC_UI();
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
        ActiveESC_UI();
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

    // CombinationUI
    public bool IsActiveCombinationUI()
    {
        return CombinationUI.activeSelf;
    }
    public void ActiveCombinationUI()
    {
        CombinationUI.SetActive(true);
    }
    public void DeActiveCombinationUI()
    {
        CombinationUI.SetActive(false);
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

    // CenterDot
    public void ActiveCenterDot()
    {
        centerDot.SetActive(true);
    }
    public void DeActiveCentorDot()
    {
        centerDot.SetActive(false);
    }


    // ESC_UI
    public bool IsActiveESC_UI()
    {
        return ESC_UI.activeSelf;
    }
    public void ActiveESC_UI()
    {
        ESC_UI.SetActive(true);
    }
    public void DeActiveESC_UI()
    {
        ESC_UI.SetActive(false);
    }

    // Deactive HintImage
    public bool IsActiveHintUI()
    {
        return HintImage.activeSelf;
    }
    public void DeActiveHintImage()
    {
        DeActiveESC_UI();
        HintImage.SetActive(false);
    }
    public void ActiveHintImage(int ID)
    {
        ActiveESC_UI();
        HintImage.GetComponent<Image>().sprite = HintSprites[ID];
        HintImage.SetActive(true);
    }
}
