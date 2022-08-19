using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Investigation : MonoBehaviour
{
    private Camera mainCamera;
    private float rayDistance = 2f;
    public GameObject pickupUI;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    Outline currentOutline;

    public void RayFromCamera()
    {
        Ray ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        //int layerMask = 1 << 6; // player layer

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                pickupUI.SetActive(true);
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                pickupUI.SetActive(true);
                currentOutline = hit.transform.GetComponent<Outline>();
                currentOutline.enabled = true;
            }

            // 아이템 줍줍
            if (Input.GetKeyDown(KeyCode.F) && pickupUI.activeSelf == true && hit.transform.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                hit.transform.GetComponent<ItemPickup>().Pickup();

            }

            // 오브젝트 애니메이션 활성화
            if (Input.GetKeyDown(KeyCode.F) && pickupUI.activeSelf == true && hit.transform.CompareTag("Object1") && hit.transform.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                ChangeObject(hit); // 버튼 눌렀을 때 오브젝트에 변화주기
            }
        }

        else
        {
            if (currentOutline != null)
            {
                currentOutline.enabled = false;
            }

            pickupUI.SetActive(false);
        }
    }

    private void ChangeObject(RaycastHit hit)
    {
        Animator ObjAni = hit.transform.gameObject.GetComponent<Animator>();
        ObjAni.SetBool("isActive", true);
        StartCoroutine(ResetObject(ObjAni));
    }

    IEnumerator ResetObject(Animator ObjAni)
    {
        yield return new WaitForSeconds(5f);
        ObjAni.SetBool("isActive", false);
    }
}