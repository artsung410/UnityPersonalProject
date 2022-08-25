using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;


public class Enigma : MonoBehaviour
{
    public static Enigma Instance;
    public static int currentSceneID;
    [SerializeField] private TextMeshPro[] text;
    private int[] result, correctCombination;
    public bool IsZoomIn;
    public bool IsCorrectRotor;

    public static event Action KeyChangeSignal = delegate { };
    [SerializeField] private KeyOutController keyContorl;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        result = new int[] { 0, 0, 0 };
        correctCombination = new int[] { 5, 0, 9 };
        RotateGear.RotatedGear += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "Rotor_left":
                result[0] = number;
                text[0].text = number.ToString();
                break;
            case "Rotor_Middle":
                result[1] = number;
                text[1].text = number.ToString();

                break;
            case "Rotor_Right":
                result[2] = number;
                text[2].text = number.ToString();
                break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2])
        {
            Debug.Log("Opened!");
            IsCorrectRotor = true;
        }
    }

}
