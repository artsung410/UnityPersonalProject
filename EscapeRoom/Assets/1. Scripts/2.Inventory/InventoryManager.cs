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
    [SerializeField] private PlayerHUD playerHUD;
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    public bool IsGripItem = false;
    public GameObject CurrentGripItemPrefab; 
    public Item CurrentGripItem;

    [SerializeField] private Image SelectImage;
    [Header("DetailViewInventoryMode")]
    [SerializeField] private Transform DetailsTransform;
    public GameObject CurrentDetailsViewItem;

    [Header("EnigmaInfo")]
    public int EnigmaToolsCount;
    public Item enigmaItem;
    public bool IsEnigmaAssembled;
    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        if (item.id == 5 || item.id == 6 || item.id == 7 || item.id == 8)
        {
            ++EnigmaToolsCount;

            if (EnigmaToolsCount == 4)
            {
                Add(enigmaItem);
                IsEnigmaAssembled = true;
                EnigmaToolsDataRemove();
                EnigmaToolsCount = 0;
            }
        }

        if (IsEnigmaAssembled == true && ((item.id == 5 && item.id == 6 || item.id == 7 || item.id == 8)))
        {
            return;
        }

        Items.Add(item);
        playerHUD.ActiveGetItemUI(item);
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

    // �󼼺��� ��ư Ŭ���� �̺�Ʈ
    public void SwitchToDetailsView()
    {
        foreach (Item item in Items)
        {
            if (item.name == SelectImage.sprite.name)
            {
                // �ִϱ׸��ϼ�ǰ�϶� ��ưŬ���� �ִϱ׸� ī�޶�� ��ȯ�Ѵ�. 
                if (SelectImage.sprite.name == "Enigma_Full")
                {
                    CameraManager.Instance.InitEnigmaViewCamera();
                    return;
                }

                if (CurrentDetailsViewItem != null)
                {
                    Destroy(CurrentDetailsViewItem);
                }

                // ������ ī�޶� ��ȯ�� ������Ʈ�� ��ġ���ش�.
                Transform hand = DetailsTransform;
                GameObject SelectItem = Instantiate(item.prefab, hand);
                SelectItem.transform.position = DetailsTransform.position;
                CurrentDetailsViewItem = SelectItem;
            }
        }

        CameraManager.Instance.InitDetailViewCamera();
    }

    private void EnigmaToolsDataRemove()
    {
        int size = Items.Count;

        for (int item = Items.Count - 1; item >=0; item--)
        {
            if (Items[item].id == 5 || Items[item].id == 6 || Items[item].id == 7 || Items[item].id == 8)
            {
                Items.Remove(Items[item]);
            }
        }
    }
}
