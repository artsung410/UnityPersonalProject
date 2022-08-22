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

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    public bool IsGripItem = false;
    public GameObject CurrentGripItemPrefab; 
    public Item CurrentGripItem;
    [HideInInspector] public Camera mainCamera;

    [Header("DetailViewInventoryMode")]
    [SerializeField] private Image SelectImage;
    [SerializeField] private Transform DetailsTransform;
    public Camera DetailViewCamera;
    public Vector3 prevCameraPos;
    [HideInInspector] public bool IsActiveDetailViewCamera = false;
    public GameObject CurrentDetailsViewItem;

    private void Awake()
    {
        prevCameraPos = DetailViewCamera.gameObject.transform.position;
        Instance = this;
        mainCamera = Camera.main;
    }

    public void Add(Item item)
    {
        Items.Add(item);
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

    public void SwitchToDetailsView()
    {
        foreach (Item item in Items)
        {
            if (item.name == SelectImage.sprite.name)
            {
                if (CurrentDetailsViewItem != null)
                {
                    Destroy(CurrentDetailsViewItem);
                }

                Transform hand = DetailsTransform;
                GameObject SelectItem = Instantiate(item.prefab, hand);
                SelectItem.transform.position = DetailsTransform.position;
                CurrentDetailsViewItem = SelectItem;
            }
        }

        mainCamera.gameObject.SetActive(false);
        DetailViewCamera.gameObject.SetActive(true);
        IsActiveDetailViewCamera = true;
    }
}
