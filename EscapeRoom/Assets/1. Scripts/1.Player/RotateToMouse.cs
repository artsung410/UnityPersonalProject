using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField] private float rotCamXAxisSpeed = 5f; // 카메라 x축 회전속도
    [SerializeField] private float rotCamYAxisSpeed = 3f; // 카메라 y축 회전속도
    [SerializeField] private GameObject Camera;

    private float limitMinX = -100; // 카메라 x축 회전 범위 (최소)
    private float limitMaxX = 70; // 카메라 x축 회전 범위 (최대)

    private float eulerAngleX; // 마우스 좌 / 우 이동으로 카메라 y축 회전
    private float eulerAngleY; // 마우스 위 / 아래 이동으로 카메라 x축 회전

    public void CalculateRotation(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotCamYAxisSpeed;
        eulerAngleX -= mouseY * rotCamXAxisSpeed;
        eulerAngleX = Mathf.Clamp(eulerAngleX, limitMinX, limitMaxX);
        Camera.transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
        transform.rotation = Quaternion.Euler(0, eulerAngleY, 0);
    }

    // 카메라 x축 회전의 경우 회전 범위를 설정
}