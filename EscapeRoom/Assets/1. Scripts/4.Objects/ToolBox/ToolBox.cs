using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : InterectiveObject
{
    [Header("ToolBox")]
    [SerializeField] private float activeTime_AfterCompletion;
    private BoxCollider boxCol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCol = gameObject.GetComponent<BoxCollider>();
        DeActiveBoxCollider(boxCol);
    }

    public override void Operate()
    {
        if (isOpened)
        {
            isActive = true;
            animator.SetBool("isActive", true);
            StartCoroutine(reset(activeTime_AfterCompletion));
        }

        else
        {
            if (LockController.isNumLockOpen == true)
            {
                isActive = true;
                animator.SetBool("isActive", true);
                StartCoroutine(resetOnlyAnimation(activeTime));
            }
        }
    }
}
