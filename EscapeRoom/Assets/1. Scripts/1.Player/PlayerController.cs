using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMouseController
{
    private RotateToMouse RotateToMouse; // ���콺 �̵����� ī�޶� ȸ��
    private PlayerMovement PlayerMovement;
    private Investigation Investigating;

    private PlayerHUD playerHUD;

    [SerializeField] private DetailCameraController CameraController;

    KeyCode ESC = KeyCode.Escape;
    KeyCode Inventory = KeyCode.I;



    [Header("Audio Clips")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipWalk;                      // �ȱ� ����

    public Item[] ItemList;
    public Item EnigmaItem;

    void Awake()
    {
        playerHUD = GetComponent<PlayerHUD>();
        RotateToMouse = GetComponent<RotateToMouse>();
        PlayerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        Investigating = GetComponent<Investigation>();

        MouseCursorLock();
    }

    void FixedUpdate()
    {
        if (CameraManager.Instance.Cameras[0].enabled == true && false == playerHUD.IsActiveInventoryUI() && false == playerHUD.IsActiveGetItemUI())
        {
            UpdateRaycasting();
            UpdateRotate();
            UpdateMove();
        }
    }

    void Update()
    {
        // ġƮŰ

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < ItemList.Length; i++)
            {
                InventoryManager.Instance.Add(ItemList[i]);
            }
            InventoryManager.Instance.Add(EnigmaItem);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (playerHUD.IsActiveGetItemUI() == true && playerHUD.IsReadyToDeactiveGetItemUI == true)
            {
                playerHUD.DeActiveGetItemUI();
            }
        }

        if (Input.GetKeyDown(ESC))
        {
            // getItemUI�� ȭ�鿡 ���ְ� ESC��ư�� ������ �� ��Ȱ��ȭ
            if (playerHUD.IsActiveGetItemUI())
            {
                playerHUD.DeActiveGetItemUI();
            }

            if (playerHUD.IsActiveInventoryUI())
            {
                playerHUD.DeActiveInventoryUI();
            }

            if (playerHUD.IsActiveCombinationUI())
            {
                playerHUD.DeActiveCombinationUI();
            }

            CameraManager.Instance.InitMainCamera();
        }

        // <����ī�޶��϶��� ���� �κ��丮�� ��ȿ>
        if (CameraManager.Instance.Cameras[0].enabled == true && CameraManager.Instance.Cameras[2].enabled == false)
        {
            UpdateInventory();
        }

        // <�ι�° ī�޶��϶��� ���� �κ��丮�� ��ȿ>
        else if (CameraManager.Instance.Cameras[1].enabled == true)
        {
            UpdateSubInventory();
        }

        // [PlayerHUD.cs] ī�޶� ��ȯ�ɶ��� pickupUI�� ������ �Ѵ�.
        if (CameraManager.Instance.Cameras[1].enabled == true || CameraManager.Instance.Cameras[2].enabled == true)
        {
            if (playerHUD.IsActiveLockedUI() == true || playerHUD.IsMouseClikedUI() == true)
            {
                playerHUD.DeActiveLockedUI();
                playerHUD.DeActiveMouseClickUI();
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

        if (x != 0 || z != 0)
        {
            ActiveMoveSound();
        }

        else
        {
            DeActiveMoveSound();
        }

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
        if (Input.GetKeyDown(Inventory))
        {
            DeActiveMoveSound();

            if (playerHUD.IsActiveInventoryUI() == false)
            {
                playerHUD.ActiveInventoryUI();
                InventoryManager.Instance.ListItems();
            }
            else
            {
                playerHUD.DeActiveInventoryUI();
            }
        }

        if (playerHUD.IsActiveInventoryUI() == true)
        {
            MouseCursorUnLock();
        }

        else
        {
            MouseCursorLock();
        }
    }

    // ###################### Section 2 ######################

    void UpdateSubInventory()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        Debug.Log($"[Mouse X] : {MouseX}");
        Debug.Log($"[MouseY] : {MouseY}");

        // ���� ���콺 Ŭ���� ���콺�� �����϶� ������Ʈ ȸ��
        if (Input.GetMouseButton(0))
        {
            RotateDetailsItem(MouseX, MouseY);
        }

        ZoomToDetailsItem();
    }

    float speed = 3f;
    void RotateDetailsItem(float x, float y)
    {
        Transform ItemTransform = InventoryManager.Instance.CurrentDetailsViewItem.transform;
        ItemTransform.Rotate(-y * speed, -x * speed, 0f, Space.World);
    }

    void ZoomToDetailsItem()
    {
        float t_zoomDirection = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log($"[Zoom] : {t_zoomDirection}");

        CameraController.ZoomInOut(t_zoomDirection);
    }

    private void ResetCurrentInventoryMode()
    {
        CameraManager.Instance.InitMainCamera();
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

    public void ActiveMoveSound()
    {
        audioSource.clip = audioClipWalk;

        if (audioSource.isPlaying == false)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void DeActiveMoveSound()
    {
        if (audioSource.isPlaying == true)
        {
            audioSource.Stop();
        }
    }
}