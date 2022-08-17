using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Item
{
    Lench,
    Note,
    Silver_Key,
    Gold_key,
    ScrewDriver,
    IronCross,
    Enigma_Battery,
    Enigma_Gear,
    Enigma_Keyboard,
    Enigma_Box,
}

public class Investigation : MonoBehaviour
{
    private Camera mainCamera;
    private float rayDistance = 1f;
    public GameObject pickupUI;

    public RectTransform InventoryRect;

    // IMAGE 처리
    public GameObject[] items;
    public Queue<Vector2> itemPositions;

    // Item 처리
    private string[] itemNames = { "Lench", "Note", "Silver_Key", "Gold_key", "ScrewDriver", "IronCross", "Enigma_Battery", "Enigma_Gear", "Enigma_Keyboard", "Enigma_Box"};
    private int maxItemNum = 10;

    void Awake()
    {
        mainCamera = Camera.main;
        itemPositions = new Queue<Vector2>();
        CreateItmePos();
    }

    private void CreateItmePos()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                itemPositions.Enqueue(new Vector2(80 * j + 60, -70 * i - 50));
            }
        }
    }

    public void RayFromCamera()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        //int layerMask = 1 << 6; // player layer

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                pickupUI.SetActive(true);
            }

            PickUpItem(hit); // 버튼 눌렀을때 아이템 변화주기
            ChangeObject(hit); // 버튼 눌렀을 때 오브젝트에 변화주기
        }
        else
        {
            pickupUI.SetActive(false);
        }
    }

    private void PickUpItem(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.F) && pickupUI.activeSelf == true)
        {
            for (int i = 0; i < maxItemNum; i++)
            {
                if (hit.transform.CompareTag(itemNames[i]))
                {
                    PutInItemInventory(i);
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    private void ChangeObject(RaycastHit hit)
    {

    }

    private void PutInItemInventory(int type)
    {
        GameObject image = Instantiate(items[type], GameObject.Find("InventoryImage").transform);

        RectTransform itemRect = image.GetComponent<RectTransform>();
        itemRect.sizeDelta = new Vector2(60, 35); // 사이즈 줄이고
        itemRect.SetAnchor(AnchorPresets.TopLeft);

        if (itemPositions.Count != 0)
        {
            Vector2 imageDir = itemRect.anchoredPosition + itemPositions.Dequeue();
            image.transform.position = imageDir;
            image.SetActive(true);
        }
        else
        {
            return; 
        }
    }
}