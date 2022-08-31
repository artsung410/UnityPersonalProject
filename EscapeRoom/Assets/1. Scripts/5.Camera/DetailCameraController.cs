using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailCameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0f;
    [SerializeField] private float zoom_Max;
    [SerializeField] private float zoom_Min;

    public void ZoomInOut(float zoomDirection)
    {
        Vector3 itemPos = InventoryManager.Instance.CurrentDetailsViewItem.transform.position;
        Vector3 dir = itemPos - transform.position;

        if (dir.z > zoom_Max && zoomDirection > 0)
        {
            dir.z = zoom_Max; return;
        }

        if(dir.z < zoom_Min && zoomDirection < 0)
        {
            dir.z = zoom_Min; return;
        }

        else
        {
            transform.position += dir * zoomDirection * zoomSpeed * Time.deltaTime;
        }
    }
}