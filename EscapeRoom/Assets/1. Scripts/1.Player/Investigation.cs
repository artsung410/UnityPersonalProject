using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Investigation : MonoBehaviour
{
    private Camera mainCamera;
    private float _RayDistance = 5f;

    KeyCode Confirm = KeyCode.F;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    Outline currentOutline;

    public void RayFromCamera()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * _RayDistance, Color.red);

        // int layerMask = 1 << 6; // player layer
        if (Physics.Raycast(ray, out hit, _RayDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                InterectiveObject interectObj = hit.transform.gameObject.GetComponent<InterectiveObject>();
                interectObj.PopUpMessage();
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Object2"))
            {
                PlayerHUD.Instance.ActiveMouseClickUI();
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Item") || hit.transform.CompareTag("Hint"))
            {
                currentOutline = hit.transform.GetComponent<Outline>();
                currentOutline.enabled = true;
            }

            // ������Ʈ1 Ŭ���� ��� Ȱ��ȭ �Ұ���..
            if (Input.GetMouseButtonDown(0) && (PlayerHUD.Instance.IsMouseClikedUI() == true || PlayerHUD.Instance.IsActiveLockedUI() == true) && (hit.transform.CompareTag("Object1") || hit.transform.CompareTag("Hint")) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                InterectiveObject interectObj = hit.transform.gameObject.GetComponent<InterectiveObject>();
                interectObj.Operate();
            }

        }

        else
        {
            if (currentOutline != null)
            {
                currentOutline.enabled = false;
            }

            PlayerHUD.Instance.DeActiveMouseClickUI();
            PlayerHUD.Instance.DeActiveLockedUI();
        }

        // ������Ʈ Ȱ��ȭ �ȵǾ��� ��, �ٸ��� Ŭ���� ���� ��� �ִ� �������� ��Ȱ��ȭ �ǵ��� ó��.
        if (InventoryManager.Instance.CurrentGripItemPrefab != null && PlayerHUD.Instance.IsMouseClikedUI() == false && Input.GetMouseButtonDown(0) || Input.GetKeyDown(Confirm))
        {
            InventoryManager.Instance.CurrentGripItemPrefab.SetActive(false);
            InventoryManager.Instance.CurrentGripItemPrefab = null;
            InventoryManager.Instance.CurrentGripItem = null;
        }
    }
}