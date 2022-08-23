using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : InterectiveObject
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Operate()
    {
        isActive = true;
        animator.SetBool("isActive", true);
        StartCoroutine(reset());
    }
}
