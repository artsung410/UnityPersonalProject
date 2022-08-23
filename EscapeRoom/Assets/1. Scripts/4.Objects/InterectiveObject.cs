using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterectiveObject : MonoBehaviour
{
    [Header("InterectiveObject")]
    public float activeTime;

    public bool isActive; 
    protected Animator animator;


    public abstract void Operate();

    protected IEnumerator reset()
    {
        yield return new WaitForSeconds(activeTime);
        isActive = false;
        animator.SetBool("isActive", false);
    }

    public void resetImmediately()
    {
        StopCoroutine(reset());
        isActive = false;
        animator.SetBool("isActive", false);
    }
}
