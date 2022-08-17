using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Item
{
    Chair,
    Table,
    Box,
    Silver_key,
    Lench,
    Password_paper,
    Anigma_gear,
    Anigma_keyBord,
    Anigma_box,
    Driver
}


public class Raycasting : MonoBehaviour
{
    private Camera mainCamera;
    private float rayDistance = 1f;
    public GameObject pickupUI;

    public RectTransform InventoryRect;

    // IMAGE 처리
    public GameObject[] items;
    public Queue<Vector2> itemPositions;

    private Dictionary<int, string> itemDic;
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

            GetItem(hit);
        }
        else
        {
            pickupUI.SetActive(false);
        }
    }

    private void GetItem(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.F) && pickupUI.activeSelf == true)
        {
            if (hit.transform.CompareTag("Chair"))
            {
                SetItemInventory((int)Item.Chair);
                hit.transform.gameObject.SetActive(false);
            }

            if (hit.transform.CompareTag("Table"))
            {
                SetItemInventory((int)Item.Table);
                hit.transform.gameObject.SetActive(false);
            }

            if (hit.transform.CompareTag("Box"))
            {
                SetItemInventory((int)Item.Box);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    private void SetItemInventory(int type)
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