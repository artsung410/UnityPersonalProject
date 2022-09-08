using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField] private float mRotCamXAxisSpeed = 5f;
    [SerializeField] private float mRotCamYAxisSpeed = 3f;
    [SerializeField] private GameObject Camera;

    private float mLimitMinX = -100;
    private float mLimitMaxX = 70; 

    private float mEulerAngleX;
    private float mEulerAngleY; 

    public void CalculateRotation(float mouseX, float mouseY)
    {
        mEulerAngleY += mouseX * mRotCamYAxisSpeed;
        mEulerAngleX -= mouseY * mRotCamXAxisSpeed;
        mEulerAngleX = Mathf.Clamp(mEulerAngleX, mLimitMinX, mLimitMaxX);
        Camera.transform.rotation = Quaternion.Euler(mEulerAngleX, mEulerAngleY, 0);
        transform.rotation = Quaternion.Euler(0, mEulerAngleY, 0);
    }
}