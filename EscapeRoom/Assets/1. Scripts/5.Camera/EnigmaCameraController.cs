using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaCameraController : MonoBehaviour
{
    private string[] aniNames = { "onTop", "onMiddle", "onBottom" };
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EnigmaCollider.transmitID += Active;
    }

    private void Active(int id)
    {
        animator.SetBool(aniNames[id], true);
        Enigma.currentSceneID = id;
    }

    public void DeActive()
    {
        animator.SetBool(aniNames[Enigma.currentSceneID], false);
    }
}
