using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterectiveObject : MonoBehaviour
{
    [Header("InterectiveObject")]
    public float activeTime;
    public string NeedItemName;

    public bool isActive;
    public bool isOpened;
    protected Animator animator;


    public abstract void Operate();

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
