using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockController : MonoBehaviour
{
    [SerializeField] private NumLock numLock;
    private int[] result, correctCombination;
    public static bool isNumLockOpen;

    private void Start()
    {
        result = new int[] { 3, 3, 3 };
        correctCombination = new int[] { 3, 7, 9 };
        RotateLock.Rotated += CheckResults;
        isNumLockOpen = false;
        //Debug.Log($"¿⁄π∞ºË Ω√¿€");
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "left":
                result[0] = number;
                //Debug.Log($"left{number}");
                break;
            case "mid":
                result[1] = number;
                //Debug.Log($"mid{number}");
                break;
            case "right":
                result[2] = number;
                //Debug.Log($"right{number}");
                break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2])
        {
            Debug.Log("Opened!");  
            isNumLockOpen = true;
            numLock.Operate();
            Destroy(numLock.gameObject, 1.5f);

        }
    }

    private void OnDestroy()
    {
        RotateLock.Rotated -= CheckResults;
    }
}
