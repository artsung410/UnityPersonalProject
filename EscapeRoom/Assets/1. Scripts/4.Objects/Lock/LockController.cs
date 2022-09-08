using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockController : MonoBehaviour
{
    [Header("NumLock")]
    [SerializeField] private NumLock numLock;
    [SerializeField] private ToolBox toolBox;
    private int[] result, correctCombination;
    public static bool isNumLockOpen;

    [Header("NormalLock")]
    public static bool isNormalLockOpen;

    private void Start()
    {
        result = new int[] { 3, 3, 3 };
        correctCombination = new int[] { 8, 4, 2 };
        RotateLock.Rotated += checkResults;
        isNumLockOpen = false;
        //Debug.Log($"¿⁄π∞ºË Ω√¿€");
    }

    private void checkResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "left":
                result[0] = number;
                break;
            case "mid":
                result[1] = number;
                break;
            case "right":
                result[2] = number;
                break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2])
        {
            Debug.Log("Opened!");  
            isNumLockOpen = true;
            numLock.Operate();
            StartCoroutine(destroyNumLock());
        }
    }

    private void OnDestroy()
    {
        RotateLock.Rotated -= checkResults;
    }

    private IEnumerator destroyNumLock()
    {
        yield return new WaitForSeconds(1f);
        Destroy(numLock.gameObject);
        toolBox.GetComponent<BoxCollider>().enabled = true;
    }
}
