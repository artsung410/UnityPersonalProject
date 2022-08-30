using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip narrAudioClip;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = narrAudioClip;
        audioSource.Play();
        Debug.Log(narrAudioClip.length);
    }

    public void DeActiveSound()
    {
        audioSource.Stop();
    }
}
