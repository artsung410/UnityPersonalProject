using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Investigation : MonoBehaviour
{
    private Camera mainCamera;
    private float rayDistance = 5f;
    private PlayerHUD playerHUD;

    KeyCode Confirm = KeyCode.F;

    void Awake()
    {
        mainCamera = Camera.main;
        playerHUD = GetComponent<PlayerHUD>();
    }

    Outline currentOutline;

    public void RayFromCamera()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        // int layerMask = 1 << 6; // player layer

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                InterectiveObject interectObj = hit.transform.gameObject.GetComponent<InterectiveObject>();
                interectObj.PopUpMessage();
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Object2"))
            {
                playerHUD.ActiveMouseClickUI();
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                currentOutline = hit.transform.GetComponent<Outline>();
                currentOutline.enabled = true;
            }

            // ������Ʈ1 �ִϸ��̼� Ȱ��ȭ (getKeyDown - F��ư�� ������ ������Ʈ�� ��ȭ��Ŵ, ���̴� �׵θ� ���� o)
            if (Input.GetMouseButtonDown(0) && (playerHUD.IsMouseClikedUI() == true || playerHUD.IsActiveLockedUI() == true) && hit.transform.CompareTag("Object1") && hit.transform.gameObject.layer == LayerMask.NameToLayer("Object"))
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

            playerHUD.DeActiveSelectedUI();
            playerHUD.DeActiveMouseClickUI();
            playerHUD.DeActiveLockedUI();
        }

        // ������Ʈ Ȱ��ȭ �ȵǾ��� ��, �ٸ��� Ŭ���� ���� ��� �ִ� �������� ���ŵǵ��� ����
        if (InventoryManager.Instance.CurrentGripItemPrefab != null && playerHUD.IsActiveSelectedUI() == false && Input.GetMouseButtonDown(0) || Input.GetKeyDown(Confirm))
        {
            Destroy(InventoryManager.Instance.CurrentGripItemPrefab);
            InventoryManager.Instance.CurrentGripItem = null;
        }
    }
}