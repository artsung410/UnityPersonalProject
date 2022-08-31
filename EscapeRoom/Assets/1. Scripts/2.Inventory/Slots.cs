using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Slots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<Sprite> onCursorEnterEvent = delegate { };
    public static event Action onCursorExitEvent = delegate { };
    public static event Action onButtonClickEvent = delegate { };

    private Image ItemIcon;
    private Camera mainCamera;

    void Awake()
    {
        ItemIcon = GetComponent<Image>();
        mainCamera = Camera.main;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Sprite ItemImage = ItemIcon.sprite;
        onCursorEnterEvent.Invoke(ItemImage);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onCursorExitEvent.Invoke();
    }

    public void ButtonClicking()
    {
        if (InventoryManager.Instance.CurrentGripItemPrefab != null)
        {
            InventoryManager.Instance.CurrentGripItemPrefab.SetActive(false);
        }

        Vector3 CameraPos = mainCamera.gameObject.transform.position;

        foreach (Item item in InventoryManager.Instance.Items)
        {
            if (item.name == ItemIcon.sprite.name)
            {
                if (item.name == "Enigma_Full")
                {
                    return;
                } 

                GameObject currentPick = item.prefab;
                InventoryManager.Instance.CurrentGripItem = item;
                InventoryManager.Instance.CurrentGripItemPrefab = mainCamera.transform.GetChild(item.id).gameObject;
                InventoryManager.Instance.CurrentGripItemPrefab.SetActive(true);
                InventoryManager.Instance.IsGripItem = true;
            }
        }

        onButtonClickEvent.Invoke();
    }
}
