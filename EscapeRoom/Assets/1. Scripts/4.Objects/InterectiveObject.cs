using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class InterectiveObject : MonoBehaviour
{
    public static event Action UnLockedMessage = delegate { };
    public static event Action SelectedMessage = delegate { };

    [Header("InterectiveObject")]
    public float activeTime;
    public string NeedItemName;

    public bool isActive;
    [HideInInspector] public bool isOpened;
    protected Animator animator;
    protected AudioSource audioSource;

    public abstract void Operate();

    public void PopUpMessage()
    {
        if (NeedItemName.Length >= 1 && false == isOpened)
        {
            UnLockedMessage();
        }
        else
        {
            SelectedMessage();
        }
    }

    protected IEnumerator reset(float time)
    {
        BoxCollider boxCol = gameObject.GetComponent<BoxCollider>();
        MeshCollider meshCol = gameObject.GetComponent<MeshCollider>();

        DeActiveCollider(boxCol, meshCol);
        yield return new WaitForSeconds(time);
        isActive = false;
        isOpened = true;
        animator.SetBool("isActive", false);
        ActiveCollider(boxCol, meshCol);
    }

    protected void SwitchingBoxColliderStatus()
    {
        BoxCollider boxCol = gameObject.GetComponent<BoxCollider>();
        boxCol.enabled = isActive == true ? false : true;
    }

    protected void SwitchinMeshxColliderStatus()
    {
        MeshCollider meshCol = gameObject.GetComponent<MeshCollider>();
        meshCol.enabled = isActive == true ? false : true;
    }


    protected IEnumerator resetOnlyAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isActive = false;
        isOpened = true;
        animator.SetBool("isActive", false);
    }

    protected void DeActiveCollider(BoxCollider boxCol, MeshCollider meshCol)
    {
        if (boxCol != null)
        {
            boxCol.enabled = false;
        }
        else
        {
            if (meshCol != null)
            {
                meshCol.enabled = false;
            }
            else
            {
                return;
            }
        }
    }

    protected void DeActiveBoxCollider(BoxCollider boxCol)
    {
        if (boxCol != null)
        {
            boxCol.enabled = false;
        }
    }

    protected void ActiveCollider(BoxCollider boxCol, MeshCollider meshCol)
    {
        if (boxCol != null)
        {
            boxCol.enabled = true;
        }
        else
        {
            if (meshCol != null)
            {
                meshCol.enabled = true;
            }
            else
            {
                return;
            }
        }
    }

    protected IEnumerator Completion(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("isOpened", true);
    }

    public void resetImmediately()
    {
        StopCoroutine(reset(activeTime));
        isActive = false;
        animator.SetBool("isActive", false);
    }
}
