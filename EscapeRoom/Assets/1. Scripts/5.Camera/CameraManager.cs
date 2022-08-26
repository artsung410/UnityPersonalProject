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
    public Vector3 prevEnigmaCameraPos;

    public Camera[] Cameras;

    private void Awake()
    {
        prevDetailViewCameraPos = Cameras[1].gameObject.transform.position;
        prevEnigmaCameraPos = Cameras[2].gameObject.transform.position;
        Instance = this;
    }

    public void SwitchToMain()
    {
        // 2��° ��ġ���� �������� ��ȯ�Ҷ�, ī�޶� ��ġ �ʱ�ȭ.
        if (Cameras[1].enabled == true)
        {
            Cameras[1].gameObject.transform.position = prevDetailViewCameraPos;
        }

        // 3��° ī�޶󿡼� �������� ��ȯ�Ҷ�, ī�޶� ��ġ �ʱ�ȭ.
        else if (Cameras[2].enabled == true)
        {
            Cameras[2].gameObject.transform.position = prevEnigmaCameraPos;
        }

        Cameras[0].enabled = true;
        Cameras[1].enabled = false;
        Cameras[2].enabled = false;
        MouseCursorUnLock();
    }

    public void SwitchToDetail()
    {
        Cameras[0].enabled = false;
        Cameras[1].enabled = true;
        Cameras[2].enabled = false;
        MouseCursorLock();
    }

    public void SwitchToEnigma()
    {
        Cameras[2].enabled = true;
        Cameras[0].enabled = false;
        Cameras[1].enabled = false;
        playerHUD.ActiveEnigmaUI();
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
