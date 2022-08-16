using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private RotateToMouse RotateToMouse; // ���콺 �̵����� ī�޶� ȸ��
    private PlayerMovement PlayerMovement;
    private Raycasting Raycasting;

    [SerializeField] private GameObject InventoryUI;
    private Animator InventoryAnimator;
    public bool isActiveInventory;

    void Awake()
    {
        RotateToMouse = GetComponent<RotateToMouse>();
        PlayerMovement = GetComponent<PlayerMovement>();
        Raycasting = GetComponent<Raycasting>();
        InventoryAnimator = InventoryUI.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        UpdateRotate();
        UpdateMove();
        UpdateZoom();
        UpdateRaycasting();
        UpdateInventory();
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
        Raycasting.RayFromCamera();
    }

    void UpdateInventory()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isActiveInventory = !isActiveInventory;
            
            InventoryAnimator.SetBool("isActiveInventory", isActiveInventory);
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
}