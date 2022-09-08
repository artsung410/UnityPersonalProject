using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailCameraController : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed = 0f;
    [SerializeField] private float _zoomMax;
    [SerializeField] private float _zoomMin;

    public void ZoomInOut(float zoomDirection)
    {
        Vector3 itemPos = InventoryManager.Instance.CurrentDetailsViewItem.transform.position;
        Vector3 dir = itemPos - transform.position;

        if (dir.z > _zoomMax && zoomDirection > 0)
        {
            dir.z = _zoomMax; return;
        }

        if(dir.z < _zoomMin && zoomDirection < 0)
        {
            dir.z = _zoomMin; return;
        }

        else
        {
            transform.position += dir * zoomDirection * _zoomSpeed * Time.deltaTime;
        }
    }
}