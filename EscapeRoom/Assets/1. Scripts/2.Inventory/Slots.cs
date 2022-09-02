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
    public static event Action<Item> onTransmtItem = delegate { };

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

        if (false == PlayerHUD.Instance.IsActiveCombinationUI())
        {
            onCursorEnterEvent.Invoke(ItemImage);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onCursorExitEvent.Invoke();
    }


    // 아이템 슬롯 클릭
    public void ButtonClicking()
    {
        // 아이템 슬롯 클릭시, 조합UI가 비활성화 일때 아이템을 플레이어 손에 장착하도록 한다.
        if (false == PlayerHUD.Instance.IsActiveCombinationUI())
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

        // 아이템 슬롯 클릭시, 조합UI가 활성화 상태일 때 아이템 조합이 가능하도록 한다.
        else
        {
            foreach (Item item in InventoryManager.Instance.Items)
            {
                if (item.name == ItemIcon.sprite.name)
                {
                    if (item.name == "Enigma_Full")
                    {
                        return;
                    }

                    onTransmtItem.Invoke(item);
                }
            }
        }
    }
}
