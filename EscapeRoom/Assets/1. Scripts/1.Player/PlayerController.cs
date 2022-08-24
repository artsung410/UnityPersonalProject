using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMouseController
{
    private RotateToMouse RotateToMouse; // ���콺 �̵����� ī�޶� ȸ��
    private PlayerMovement PlayerMovement;
    private Investigation Investigating;

    [SerializeField] private PlayerHUD playerHUD;
    [SerializeField] private CameraController CameraController;

    KeyCode ESC = KeyCode.Escape;
    KeyCode SpaceBar = KeyCode.Space;

    public Item enigmaItem;

    void Awake()
    {
        RotateToMouse = GetComponent<RotateToMouse>();
        PlayerMovement = GetComponent<PlayerMovement>();
        Investigating = GetComponent<Investigation>();

        MouseCursorLock();
    }

    void Update()
    {
        // ġƮŰ (�ӽ�)
        if (Input.GetKeyDown(KeyCode.O))
        {
            InventoryManager.Instance.Add(enigmaItem);
        }

        // [PlayerHUD.cs] ī�޶� ��ȯ�ɶ��� pickupUI�� ������ �Ѵ�.
        if (InventoryManager.Instance.IsActiveEnigmaViewCamera || InventoryManager.Instance.IsActiveDetailViewCamera)
        {
            if (playerHUD.PickUpUI.activeSelf == true)
            {
                playerHUD.DeActivePickUpUI();
            }
        }

        if (false == InventoryManager.Instance.IsActiveDetailViewCamera && false == InventoryManager.Instance.IsActiveEnigmaViewCamera)
        {
            UpdateInventory();
        }

        else if (false == InventoryManager.Instance.IsActiveEnigmaViewCamera)
        {
            UpdateSubInventory();
        }



        if (false == playerHUD.isActiveInventory && false == InventoryManager.Instance.IsActiveDetailViewCamera && false == InventoryManager.Instance.IsActiveEnigmaViewCamera)
        {
            UpdateRaycasting();
            UpdateRotate();
            UpdateMove();
        }
    }

    // ###################### Section 1 ######################
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

    // [Investigation.cs]
    void UpdateRaycasting()
    {
        Investigating.RayFromCamera();
    }

    // [PlayerHUD.cs]
    void UpdateInventory()
    {
        // �κ��丮 UI Ȱ��ȭ
        if (Input.GetKeyDown(SpaceBar) || playerHUD.isActiveInventory && Input.GetKeyDown(ESC))
        {
            playerHUD.isActiveInventory = !playerHUD.isActiveInventory;
            playerHUD.InventoryUI.SetActive(playerHUD.isActiveInventory);
            InventoryManager.Instance.ListItems();
        }

        if (playerHUD.isActiveInventory)
        {
            MouseCursorUnLock();
        }
        else
        {
            playerHUD.DeActiveItemInfo();
            MouseCursorLock();
        }
    }


    // ###################### Section 2 ######################

    void UpdateSubInventory()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        Debug.Log($"mousX : { MouseX}");
        Debug.Log($"mouseY : { MouseY}");

        // �κ��丮 �����Ϻ� ��Ȱ��ȭ
        if (Input.GetKeyDown(ESC) && true == InventoryManager.Instance.IsActiveDetailViewCamera)
        {
            ResetDetailsInventory();
        }

        // ���� ���콺 Ŭ���� ���콺�� �����϶� ������Ʈ ȸ��
        if (Input.GetMouseButton(0))
        {
            RotateDetailsItem(MouseX, MouseY);
        }

        // ���콺 �ٹ�ư �����鼭 �����϶� ������Ʈ �¿�, ���Ʒ� ������
        if (Input.GetMouseButton(2))
        {
            MoveDetailItem(MouseX, MouseY);
        }

        ZoomToDetailsItem();
    }

    float speed = 3f;
    void RotateDetailsItem(float x, float y)
    {
        Transform ItemTransform = InventoryManager.Instance.CurrentDetailsViewItem.transform;
        ItemTransform.Rotate(-y * speed, -x * speed, 0f, Space.World);
    }

    void MoveDetailItem(float x, float y)
    {
        Transform ItemTransform = InventoryManager.Instance.CurrentDetailsViewItem.transform;
        ItemTransform.position += new Vector3(-x, y, 0f) * Time.deltaTime * speed;
    }

    void ZoomToDetailsItem()
    {
        float t_zoomDirection = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(t_zoomDirection);

        CameraController.ZoomInOut(t_zoomDirection);
    }

    private void ResetDetailsInventory()
    {
        InventoryManager.Instance.DetailViewCamera.gameObject.transform.position = InventoryManager.Instance.prevCameraPos;
        InventoryManager.Instance.mainCamera.gameObject.SetActive(true);
        InventoryManager.Instance.DetailViewCamera.gameObject.SetActive(false);
        InventoryManager.Instance.IsActiveDetailViewCamera = false;
        playerHUD.isActiveInventory = true;
        playerHUD.InventoryUI.SetActive(true);
    }

    // ###################### [IMouseController] Mouse Control ######################

    public void MouseCursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MouseCursorUnLock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}