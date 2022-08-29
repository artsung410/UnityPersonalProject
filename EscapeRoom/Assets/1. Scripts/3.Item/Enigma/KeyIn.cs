using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyIn : MonoBehaviour
{
    [SerializeField] private int Id;

    public static event Action<int> KeyChangeSignal = delegate { };
    public static event Action<int> KeySignal = delegate { };

    Dictionary<int, int> CorrectKeys;
    private AudioSource audioSource;

    void Awake()
    {
        CorrectKeys = new Dictionary<int, int>();
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    private void Start()
    {
        SetCorrectKey();
    }

    private void OnMouseDown()
    {
        KeySignal(Id);
        StartCoroutine(keyboardDown());
    }

    int ChangedID;
    IEnumerator keyboardDown()
    {
        Debug.Log(Id);
        SoundManager.Instance.PlayObjectSound(audioSource, "KeyPush");
        if (false == Enigma.Instance.IsCorrectRotor)
        {
            ChangedID = ChangeStrangeKey(Id);

        }
        else
        {
            ChangedID = ChangeCorrectKey(Id);
        }

        KeyChangeSignal(ChangedID);

        yield return null;
    }

    private int ChangeStrangeKey(int key)
    {
        int randNum = UnityEngine.Random.Range(1, 100) + key;
        int randID = randNum % 26;

        return randID;
    }

    private int ChangeCorrectKey(int key)
    {
        int CorrectId = CorrectKeys[key];
        return CorrectId;
    }

    private void SetCorrectKey()
    {
        for (int i = 0; i < 26; i++)
        {
            CorrectKeys.Add(i, 25 - i);
        }
    }
}
