using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Item
{
    chair,
    table,
    box,
    silver_key,
    lench,
    password_paper,
    Anigma_gear,
    Anigma_keyBord,
    Anigma_box,
    driver
}


public class Raycasting : MonoBehaviour
{
    private Camera mainCamera;
    private float rayDistance = 1f;
    public GameObject pickupUI;

    // IMAGE Ã³¸®
    public Image[] DeActiveToolsImages;
    public Image[] ActiveToolsImages;

    void Awake()
    {
        mainCamera = Camera.main; 
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
                ActiveToolsImages[0] = DeActiveToolsImages[(int)Item.chair];
                ActiveToolsImages[0].gameObject.SetActive(true);
                hit.transform.gameObject.SetActive(false);
            }

            if (hit.transform.CompareTag("Table"))
            {
                ActiveToolsImages[1] = DeActiveToolsImages[(int)Item.table];
                ActiveToolsImages[1].gameObject.SetActive(true);
                hit.transform.gameObject.SetActive(false);
            }

            if (hit.transform.CompareTag("Box"))
            {
                ActiveToolsImages[2] = DeActiveToolsImages[(int)Item.box];
                ActiveToolsImages[2].gameObject.SetActive(true);
                hit.transform.gameObject.SetActive(false);
            }
        }
    }

    //private Image serchItem()
    //{

    //    for (int i = 0; i < ActiveToolsImages.Length; i++)
    //    {
    //        if (!ActiveToolsImages[i])
    //        {
    //            Image blankImage = ActiveToolsImages[i];
    //            return;

    //        }
    //    }
    //}


}