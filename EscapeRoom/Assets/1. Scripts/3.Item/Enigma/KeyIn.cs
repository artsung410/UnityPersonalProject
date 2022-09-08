using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyIn : MonoBehaviour
{
    public static event Action<int> KeyChangeSignal = delegate { };
    public static event Action<int> KeySignal       = delegate { };

    [SerializeField] private int Id;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        KeySignal(Id);
        StartCoroutine(keyboardDown());
    }

    int ChangedID;
    IEnumerator keyboardDown()
    {
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
        int CorrectId = 25 - key;
        return CorrectId;
    }
}
