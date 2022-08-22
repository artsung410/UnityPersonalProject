using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private RotateToMouse RotateToMouse; // ���콺 �̵����� ī�޶� ȸ��
    private PlayerMovement PlayerMovement;
    private Investigation Investigating;


    [SerializeField] private PlayerHUD playerHUD;

    void Awake()
    {
        RotateToMouse = GetComponent<RotateToMouse>();
        PlayerMovement = GetComponent<PlayerMovement>();
        Investigating = GetComponent<Investigation>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (false == InventoryManager.Instance.IsActiveDetailViewCamera)
        {
            UpdateInventory();
        }
        UpdateSubInventory();

        if (false == playerHUD.isActiveInventory && false == InventoryManager.Instance.IsActiveDetailViewCamera)
        {
            UpdateRaycasting();
            UpdateRotate();
            UpdateMove();
        }
    }

    void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        RotateToMouse.CalculateRotation(mouseX, mouseY);
    }

    void UpdateMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        PlayerMovement.MoveTo(new Vector3(x, 0, z));
    }

    void UpdateRaycasting()
    {
        Investigating.RayFromCamera();
    }

    void UpdateInventory()
    {
        // �κ��丮 UI Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerHUD.isActiveInventory = !playerHUD.isActiveInventory;
            playerHUD.InventoryUI.SetActive(playerHUD.isActiveInventory);
            InventoryManager.Instance.ListItems();
        }

        if (playerHUD.isActiveInventory)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            playerHUD.DeActiveItemInfo();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void UpdateSubInventory()
    {
        // �κ��丮 �����Ϻ� ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Escape) && true == InventoryManager.Instance.IsActiveDetailViewCamera)
        {
            InventoryManager.Instance.mainCamera.gameObject.SetActive(true);
            InventoryManager.Instance.DetailViewCamera.gameObject.SetActive(false);
            InventoryManager.Instance.IsActiveDetailViewCamera = false;
        }
    }
}