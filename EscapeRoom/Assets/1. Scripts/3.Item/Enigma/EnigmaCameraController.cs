using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaCameraController : MonoBehaviour
{
    private Dictionary<int, string> aniName;
    private Animator animator;

    private void Update()
    {
        Debug.Log(Enigma.currentSceneID);
    }
    private void Start()
    {
        aniName = new Dictionary<int, string>();
        animator = GetComponent<Animator>();
        EnigmaCollider.transmitID += Active;
        aniName.Add(0, "onTop");
        aniName.Add(1, "onMiddle");
        aniName.Add(2, "onBottom");
    }

    private void Active(int id)
    {
        animator.SetBool(aniName[id], true);
        Enigma.currentSceneID = id;
    }

    public void DeActive()
    {
        animator.SetBool(aniName[Enigma.currentSceneID], false);
    }
}
