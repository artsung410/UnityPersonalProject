using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private RotateToMouse RotateToMouse; // 마우스 이동으로 카메라 회전
    private PlayerMovement PlayerMovement;
    private Investigation Investigating;

    [SerializeField] private GameObject Inventory;

    public bool isActiveInventory;


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
        UpdateZoom();
        UpdateInventory();

        if (false == isActiveInventory)
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

    void UpdateZoom()
    {
        float t_zoomDirection = Input.GetAxis("Mouse ScrollWheel");
    }

    void UpdateRaycasting()
    {
        Investigating.RayFromCamera();
    }

    void UpdateInventory()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isActiveInventory = !isActiveInventory;
            Inventory.SetActive(isActiveInventory);
            InventoryManager.Instance.ListItems();
        }

        if (isActiveInventory)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void DeActiveInventory()
    {
        isActiveInventory = false;
        Inventory.SetActive(isActiveInventory);
    }
}