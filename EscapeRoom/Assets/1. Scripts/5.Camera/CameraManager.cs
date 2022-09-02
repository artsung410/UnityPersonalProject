using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum cam
{
    main,
    detail,
    enigma
}

public class CameraManager : MonoBehaviour, IMouseController
{
    [SerializeField] private PlayerHUD playerHUD;
    public static CameraManager Instance;
    public Vector3 prevDetailViewCameraPos;

    public Camera[] Cameras;
    private Animator EnigmaCameraAnimator;

    private void Awake()
    {
        prevDetailViewCameraPos = Cameras[1].gameObject.transform.position;
        EnigmaCameraAnimator = Cameras[2].GetComponent<Animator>();
        Instance = this;
    }

    public void InitMainCamera()
    {
        // 2��° ��ġ���� �������� ��ȯ�Ҷ�, ī�޶� ��ġ �ʱ�ȭ.
        if (Cameras[1].enabled == true)
        {
            Cameras[1].gameObject.transform.position = prevDetailViewCameraPos;
        }

        // 3��° ī�޶󿡼� �������� ��ȯ�Ҷ�, ī�޶� �ִϸ��̼� �ʱ�ȭ
        else if (Cameras[2].enabled == true)
        {
            EnigmaCameraAnimator.SetBool("onTop", false);
            EnigmaCameraAnimator.SetBool("onBottom", false);
            EnigmaCameraAnimator.SetBool("onMiddle", false);
            Enigma.Instance.InitParts();
            playerHUD.DeActiveEnigmaInitButtonUI();
        }

        Cameras[0].enabled = true;
        Cameras[1].enabled = false;
        Cameras[2].enabled = false;
        playerHUD.ActiveCenterDot();
        playerHUD.DeActiveESC_UI();
        MouseCursorUnLock();
    }

    public void InitDetailViewCamera()
    {
        Cameras[0].enabled = false;
        Cameras[1].enabled = true;
        Cameras[2].enabled = false;
        playerHUD.DeActiveCentorDot();
        playerHUD.ActiveESC_UI();
        MouseCursorLock();
    }

    public void InitEnigmaViewCamera()
    {
        Cameras[2].enabled = true;
        Cameras[0].enabled = false;
        Cameras[1].enabled = false;
        playerHUD.ActiveESC_UI();
        playerHUD.DeActiveCentorDot();
        MouseCursorUnLock();
    }

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
