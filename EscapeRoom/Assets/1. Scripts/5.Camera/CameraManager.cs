using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum eCam
{
    main,
    detail,
    enigma
}

public class CameraManager : MonoBehaviour, IMouseController
{
    public static CameraManager Instance;
    public Vector3 prevDetailViewCameraPos;

    public Camera[] Cameras;
    private Animator EnigmaCameraAnimator;

    private int _MainCam = (int)eCam.main;
    private int _DetailCam = (int)eCam.detail;
    private int _EnimgaCam = (int)eCam.enigma;

    private void Awake()
    {
        prevDetailViewCameraPos = Cameras[_DetailCam].gameObject.transform.position;
        EnigmaCameraAnimator = Cameras[_EnimgaCam].GetComponent<Animator>();
        Instance = this;
    }

    public void InitMainCamera()
    {
        // 2��° ��ġ���� �������� ��ȯ�Ҷ�, ī�޶� ��ġ �ʱ�ȭ.
        if (Cameras[_DetailCam].enabled == true)
        {
            Cameras[_DetailCam].gameObject.transform.position = prevDetailViewCameraPos;
        }

        // 3��° ī�޶󿡼� �������� ��ȯ�Ҷ�, ī�޶� �ִϸ��̼� �ʱ�ȭ
        else if (Cameras[_EnimgaCam].enabled == true)
        {
            EnigmaCameraAnimator.SetBool("onTop", false);
            EnigmaCameraAnimator.SetBool("onBottom", false);
            EnigmaCameraAnimator.SetBool("onMiddle", false);
            Enigma.Instance.InitParts();
            PlayerHUD.Instance.DeActiveEnigmaInitButtonUI();
        }

        Cameras[_MainCam].enabled = true;
        Cameras[_DetailCam].enabled = false;
        Cameras[_EnimgaCam].enabled = false;
        PlayerHUD.Instance.ActiveCenterDot();
        MouseCursorLock();
    }

    public void InitDetailViewCamera()
    {
        Cameras[_MainCam].enabled = false;
        Cameras[_DetailCam].enabled = true;
        Cameras[_EnimgaCam].enabled = false;
        PlayerHUD.Instance.DeActiveCentorDot();
        PlayerHUD.Instance.ActiveReturnButtonUI();
        MouseCursorUnLock();
    }

    public void InitEnigmaViewCamera()
    {
        Cameras[_EnimgaCam].enabled = true;
        Cameras[_MainCam].enabled = false;
        Cameras[_DetailCam].enabled = false;
        PlayerHUD.Instance.DeActiveCentorDot();
        PlayerHUD.Instance.ActiveReturnButtonUI();
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
