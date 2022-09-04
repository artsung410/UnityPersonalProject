using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMouseController
{
    private RotateToMouse RotateToMouse; // 마우스 이동으로 카메라 회전
    private PlayerMovement PlayerMovement;
    private Investigation Investigating;

    [SerializeField] private DetailCameraController CameraController;

    KeyCode ESC = KeyCode.Escape;
    KeyCode Inventory = KeyCode.I;

    [Header("Audio Clips")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipWalk;                      // 걷기 사운드

    public Item[] ItemList;
    public Item EnigmaItem;

    void Awake()
    {
        RotateToMouse = GetComponent<RotateToMouse>();
        PlayerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        Investigating = GetComponent<Investigation>();
        MouseCursorLock();
    }
    
    void Start()
    {
        PlayerHUD.Instance.ActiveCenterDot();
    }

    void Update()
    {
        if (true == CameraManager.Instance.Cameras[0].enabled && 
            false == PlayerHUD.Instance.IsActiveInventoryUI() && 
            false == PlayerHUD.Instance.IsActiveGetItemUI() && 
            false == PlayerHUD.Instance.IsActivePushedUI() &&
            false == PlayerHUD.Instance.IsActiveHintUI() && 
            false == PlayerHUD.Instance.IsActiveSettingUI())
        {
            UpdateRaycasting();
            UpdateRotate();
            UpdateMove();
        }

        // 치트키

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            for (int i = 0; i < ItemList.Length; i++)
            {
                InventoryManager.Instance.Add(ItemList[i]);
            }
            InventoryManager.Instance.Add(EnigmaItem);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerHUD.Instance.IsActiveGetItemUI() == true && PlayerHUD.Instance.IsReadyToDeactiveGetItemUI == true)
            {
                PlayerHUD.Instance.DeActiveGetItemUI();
            }
        }

        if (Input.GetKeyDown(ESC))
        {
            if (false == PlayerHUD.Instance.IsActiveHintUI() && 
                false == PlayerHUD.Instance.IsActiveGetItemUI() &&
                false == PlayerHUD.Instance.IsActiveInventoryUI() &&
                false == PlayerHUD.Instance.IsActiveSettingUI() &&
                false == CameraManager.Instance.Cameras[1].enabled && 
                false == CameraManager.Instance.Cameras[2].enabled)
            {
                PlayerHUD.Instance.SwitchingPushedUI();
                DeActiveMoveSound();
            }

            if (PlayerHUD.Instance.IsActiveHintUI()) PlayerHUD.Instance.DeActiveHintImage();

            if (PlayerHUD.Instance.IsActiveGetItemUI()) PlayerHUD.Instance.DeActiveGetItemUI();

            if (PlayerHUD.Instance.IsActiveReturnButtonUI()) PlayerHUD.Instance.DeActiveReturnButtonUI();

            if (PlayerHUD.Instance.IsActiveSettingUI()) PlayerHUD.Instance.DeActiveSettingsUI();

            if (PlayerHUD.Instance.IsActiveInventoryUI())
            {
                PlayerHUD.Instance.DeActiveInventoryUI();
                MouseCursorLock();
            }

            if (PlayerHUD.Instance.IsActiveCombinationUI()) PlayerHUD.Instance.DeActiveCombinationUI();

                
            if (CameraManager.Instance.Cameras[1].enabled || CameraManager.Instance.Cameras[2].enabled) CameraManager.Instance.InitMainCamera();
        }

        // <메인카메라일때는 기존 인벤토리만 유효>
        if (CameraManager.Instance.Cameras[0].enabled == true && CameraManager.Instance.Cameras[2].enabled == false) UpdateInventory();

        // <두번째 카메라일때는 서브 인벤토리만 유효>
        else if (CameraManager.Instance.Cameras[1].enabled == true) UpdateSubInventory();

        // [PlayerHUD.cs] 카메라 전환될때는 pickupUI를 끄도록 한다.
        if (CameraManager.Instance.Cameras[1].enabled == true || CameraManager.Instance.Cameras[2].enabled == true)
        {
            if (PlayerHUD.Instance.IsActiveLockedUI() == true || PlayerHUD.Instance.IsMouseClikedUI() == true)
            {
                PlayerHUD.Instance.DeActiveLockedUI();
                PlayerHUD.Instance.DeActiveMouseClickUI();
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
        // 인벤토리 UI 활성화
        if (Input.GetKeyDown(Inventory))
        {
            DeActiveMoveSound();

            if (PlayerHUD.Instance.IsActiveInventoryUI() == false)
            {
                PlayerHUD.Instance.ActiveInventoryUI();
                InventoryManager.Instance.ListItems();
            }
            else
            {
                PlayerHUD.Instance.DeActiveInventoryUI();
            }
        }
    }

    // ###################### Section 2 ######################

    void UpdateSubInventory()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        Debug.Log($"[Mouse X] : {MouseX}");
        Debug.Log($"[MouseY] : {MouseY}");

        // 왼쪽 마우스 클릭중 마우스를 움직일때 오브젝트 회전
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