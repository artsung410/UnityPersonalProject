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

    public InventoryItemController[]   InventoryItems;
    public Transform                   ItemContent;
    public GameObject                  InventoryItem;
    public Toggle                      EnableRemove;
    public GameObject                  CurrentGripItemPrefab; 
    public Item                        CurrentGripItem;

    [SerializeField] private Image     SelectImage;
    [SerializeField] private Transform DetailsTransform;
    public GameObject                  CurrentDetailsViewItem;

    [Header("EnigmaInfo")]
    private int  _enigmaToolsCount;
    private bool _isEnigmaAssembled;
    private bool _isGripItem;

    public int EnigmaToolsCount{ get { return _enigmaToolsCount; } set { _enigmaToolsCount = value; }}
    public bool IsEnigmaAssembled { get { return _isEnigmaAssembled; } set { _isEnigmaAssembled = value; } }
    public bool IsGripItem { get { return _isGripItem; } set { _isGripItem = value; } }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CombinationSlot.CombinationComplete += getEnigma;
    }

    private void removeEnigmaToolsData()
    {
        for (int item = Items.Count - 1; item >= 0; item--)
        {
            if (Items[item].id == 5 || Items[item].id == 6 || Items[item].id == 7 || Items[item].id == 8)
            {
                Items.Remove(Items[item]);
            }
        }
    }

    private void getEnigma(Item Enigma)
    {
        removeEnigmaToolsData();
        Add(Enigma);
        ListItems();
    }

    public void Add(Item item)
    {
        Items.Add(item);
        PlayerHUD.Instance.ActiveGetItemUI(item);
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

    // 상세보기 버튼 클릭시 이벤트
    public void SwitchToDetailsView()
    {
        foreach (Item item in Items)
        {
            if (item.name == SelectImage.sprite.name)
            {
                // 애니그마완성품일때 버튼클릭시 애니그마 카메라로 전환한다. 
                if (SelectImage.sprite.name == "Enigma_Full")
                {
                    CameraManager.Instance.InitEnigmaViewCamera();
                    return;
                }

                if (CurrentDetailsViewItem != null)
                {
                    Destroy(CurrentDetailsViewItem);
                }

                // 디테일 카메라 전환시 오브젝트를 배치해준다.
                Transform hand = DetailsTransform;
                GameObject SelectItem = Instantiate(item.prefab, hand);
                SelectItem.transform.position = DetailsTransform.position;
                CurrentDetailsViewItem = SelectItem;
            }
        }

        CameraManager.Instance.InitDetailViewCamera();
    }


}
