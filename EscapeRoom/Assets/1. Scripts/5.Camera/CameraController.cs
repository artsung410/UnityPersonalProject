using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0f;
    [SerializeField] private float zoomMax = 1f;
    [SerializeField] private float zoomMin = 5f;

    PlayerController player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    public void ZoomInOut(float zoomDirection)
    {
        Vector3 itemPos = InventoryManager.Instance.CurrentDetailsViewItem.transform.position;
        Vector3 dir = itemPos - transform.position;
        float zoom = Vector3.Distance(transform.position, itemPos);
        zoom = Mathf.Clamp(zoom, zoomMax, zoomMin);

        //if (zoom <= zoomMax && zoomDirection > 0) { zoom += 0.5f; }

        //if (zoom > zoomMin && zoomDirection < 0) { zoom -= 0.5f; };

        transform.position += dir * zoomDirection * zoomSpeed;

        Debug.Log($"zoom : {zoom}");
    }
}