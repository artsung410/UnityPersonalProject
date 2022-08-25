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
    public static CameraManager Instance;
    public Vector3 prevDetailViewCameraPos;
    public Vector3 prevEnigmaCameraPos;

    public Camera[] Cameras;

    Camera mainCam, detailCam, enigmaCam;
    [HideInInspector] public bool IsActiveDetailViewCamera = false;
    [HideInInspector] public bool IsActiveEnigmaViewCamera = false;

    private void Awake()
    {
        mainCam = Cameras[(int)cam.main];
        detailCam = Cameras[(int)cam.detail];
        enigmaCam = Cameras[(int)cam.enigma];

        prevDetailViewCameraPos = Cameras[(int)cam.detail].gameObject.transform.position;
        prevEnigmaCameraPos = Cameras[(int)cam.enigma].gameObject.transform.position;
        Instance = this;
    }

    public void SwitchToMain()
    {
        // 2번째 위치에서 메인으로 전환할때, 카메라 위치 초기화.
        if (detailCam.gameObject.activeSelf == true)
        {
            detailCam.gameObject.transform.position = prevDetailViewCameraPos;
        }

        // 3번째 카메라에서 메인으로 전환할때, 카메라 위치 초기화.
        else if (enigmaCam.gameObject.activeSelf == true)
        {
            enigmaCam.gameObject.transform.position = prevEnigmaCameraPos;
        }

        mainCam.gameObject.SetActive(true);
        detailCam.gameObject.SetActive(false);
        enigmaCam.gameObject.SetActive(false);
    }

    public void SwitchToDetail()
    {
        mainCam.gameObject.SetActive(false);
        detailCam.gameObject.SetActive(true);
        enigmaCam.gameObject.SetActive(false);
        MouseCursorLock();
    }

    public void SwitchToEnigma()
    {
        mainCam.gameObject.SetActive(false);
        detailCam.gameObject.SetActive(false);
        enigmaCam.gameObject.SetActive(true);
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
