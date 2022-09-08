using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField] private float mRotCamXAxisSpeed = 5f;
    [SerializeField] private float mRotCamYAxisSpeed = 3f;
    [SerializeField] private GameObject Camera;

    private float _LimitMinX = -100;
    private float _LimitMaxX = 70; 

    private float _EulerAngleX;
    private float _EulerAngleY; 

    public void CalculateRotation(float mouseX, float mouseY)
    {
        _EulerAngleY += mouseX * mRotCamYAxisSpeed;
        _EulerAngleX -= mouseY * mRotCamXAxisSpeed;
        _EulerAngleX = Mathf.Clamp(_EulerAngleX, _LimitMinX, _LimitMaxX);
        Camera.transform.rotation = Quaternion.Euler(_EulerAngleX, _EulerAngleY, 0);
        transform.rotation = Quaternion.Euler(0, _EulerAngleY, 0);
    }
}