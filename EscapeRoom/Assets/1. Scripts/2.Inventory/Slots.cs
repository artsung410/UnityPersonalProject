using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnCursorEnterEvent : UnityEngine.Events.UnityEvent<Sprite> { }
public class OnCursorExitEvent : UnityEngine.Events.UnityEvent { }
public class OnButtonClickEvent : UnityEngine.Events.UnityEvent { }

public class Slots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static OnCursorEnterEvent onCursorEnterEvent = new OnCursorEnterEvent();
    public static OnCursorExitEvent onCursorExitEvent = new OnCursorExitEvent();
    public static OnCursorExitEvent onButtonClickEvent = new OnCursorExitEvent();

    private Image ItemIcon;
    private Camera mainCamera;
    private View cameraView;

    void Awake()
    {
        ItemIcon = GetComponent<Image>();
        mainCamera = Camera.main;
        cameraView = mainCamera.gameObject.GetComponent<View>();
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
        Vector3 handPos = cameraView.Hand.gameObject.transform.position;
        Vector3 CameraPos = mainCamera.gameObject.transform.position;
        Transform hand = cameraView.transform;

        foreach (Item item in InventoryManager.Instance.Items)
        {
            if (item.name == ItemIcon.sprite.name)
            {
                if (InventoryManager.Instance.CurrentGripItem != null)
                {
                    Destroy(InventoryManager.Instance.CurrentGripItemPrefab);
                }

                GameObject currentPick = item.prefab;
                GameObject gripItemPrefab = Instantiate(currentPick, hand);

                InventoryManager.Instance.CurrentGripItem = item;
                InventoryManager.Instance.CurrentGripItemPrefab = gripItemPrefab;
                InventoryManager.Instance.IsGripItem = true;

                if (currentPick.CompareTag("Gold_Key") || currentPick.CompareTag("Silver_Key") || currentPick.CompareTag("Lench"))
                {
                    gripItemPrefab.transform.position = handPos;
                    Vector3 dir = handPos - CameraPos;
                    Vector3 Axis = new Vector3(0f, 1f, 0f);
                    gripItemPrefab.transform.rotation = Quaternion.LookRotation(dir);
                    gripItemPrefab.transform.rotation = Quaternion.AngleAxis(-30f, Axis);
                }

                else
                {
                    gripItemPrefab.transform.position = handPos;
                    break;
                }
            }
        }

        onButtonClickEvent.Invoke();
    }
}
