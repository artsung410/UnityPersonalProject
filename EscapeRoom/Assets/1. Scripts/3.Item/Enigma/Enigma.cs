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

    [SerializeField] private KeyOutController keyContorl;


    [SerializeField] private GameObject Rotor;
    private RotateGear Rotor_left;
    private RotateGear Rotor_middle;
    private RotateGear Rotor_right;

    [SerializeField] private GameObject ColliderSet;
    private EnigmaCollider Collider_Description;
    private EnigmaCollider Collider_Rotor;
    private EnigmaCollider Collider_KeyBoard;

    [Header("GlowingRotorNumbers")]
    [SerializeField] private GameObject[] GlowingRotorNumbers;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        Rotor_left = Rotor.transform.GetChild(0).gameObject.GetComponent<RotateGear>();
        Rotor_middle = Rotor.transform.GetChild(1).gameObject.GetComponent<RotateGear>();
        Rotor_right = Rotor.transform.GetChild(2).gameObject.GetComponent<RotateGear>();

        Collider_Description = ColliderSet.transform.GetChild(0).gameObject.GetComponent<EnigmaCollider>();
        Collider_Rotor = ColliderSet.transform.GetChild(1).gameObject.GetComponent<EnigmaCollider>();
        Collider_KeyBoard = ColliderSet.transform.GetChild(2).gameObject.GetComponent<EnigmaCollider>();

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        RotateGear.RotatedGear += CheckResults;
        InitParts();
    }

    public void InitParts()
    {
        // Camera
        IsZoomIn = false;

        // RotorInit
        if (false == IsCorrectRotor)
        {
            InitRotor();
        }

        // ColliderInit
        InitCollider();
    }

    public void InitCollider()
    {
        Collider_Description.ActiveCollider();
        Collider_Rotor.ActiveCollider();
        Collider_KeyBoard.ActiveCollider();
    }

    public void InitRotor()
    {
        IsCorrectRotor = false;
        result = new int[] { 0, 0, 0 };
        correctCombination = new int[] { 5, 0, 9 };

        Rotor_left.InitSetNumber(0);
        Rotor_middle.InitSetNumber(0);
        Rotor_right.InitSetNumber(0);

        text[0].text = "0";
        text[1].text = "0";
        text[2].text = "0";
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

            for (int i = 0; i < GlowingRotorNumbers.Length; i++)
            {
                GlowingRotorNumbers[i].SetActive(true);
            }

            SoundManager.Instance.PlayObjectSound(audioSource, "RotateUnlock");
        }
    }

}
