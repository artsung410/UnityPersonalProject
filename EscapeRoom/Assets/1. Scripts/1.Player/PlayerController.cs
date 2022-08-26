using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMouseController
{
    private RotateToMouse RotateToMouse; // 마우스 이동으로 카메라 회전
    private PlayerMovement PlayerMovement;
    private Investigation Investigating;

    [SerializeField] private PlayerHUD playerHUD;
    [SerializeField] private CameraController CameraController;

    KeyCode ESC = KeyCode.Escape;
    KeyCode Inventory = KeyCode.I;

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
        if (Input.GetKeyDown(ESC))
        {
            if (playerHUD.GetItemUI.activeSelf == true)
            {
                playerHUD.GetItemUI.SetActive(false);
            }

            CameraManager.Instance.SwitchToMain();
        }

        // <메인카메라일때는 기존 인벤토리만 유효>
        if (CameraManager.Instance.Cameras[0].enabled == true && CameraManager.Instance.Cameras[2].enabled == false)
        {
            UpdateInventory();
        }

        // <두번째 카메라일때는 서브 인벤토리만 유효>
        else if (CameraManager.Instance.Cameras[1].enabled == true)
        {
            UpdateSubInventory();
        }


        if (CameraManager.Instance.Cameras[0].enabled == true && false == playerHUD.InventoryUI.activeSelf)
        {
            UpdateRaycasting();
            UpdateRotate();
            UpdateMove();
        }

        // [PlayerHUD.cs] 카메라 전환될때는 pickupUI를 끄도록 한다.
        if (CameraManager.Instance.Cameras[1].enabled == true || CameraManager.Instance.Cameras[2].enabled == true)
        {
            if (playerHUD.PickUpUI.activeSelf == true)
            {
                playerHUD.DeActivePickUpUI();
            }
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
        // 인벤토리 UI 활성화
        if (Input.GetKeyDown(Inventory) && playerHUD.InventoryUI.activeSelf == false )
        {
            playerHUD.InventoryUI.SetActive(true);
            InventoryManager.Instance.ListItems();
        }

        if (playerHUD.InventoryUI.activeSelf == true)
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


        // 왼쪽 마우스 클릭중 마우스를 움직일때 오브젝트 회전
        if (Input.GetMouseButton(0))
        {
            RotateDetailsItem(MouseX, MouseY);
        }

        // 마우스 휠버튼 누르면서 움직일때 오브젝트 좌우, 위아래 움직임
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

    private void ResetCurrentInventoryMode()
    {
        CameraManager.Instance.SwitchToMain();
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