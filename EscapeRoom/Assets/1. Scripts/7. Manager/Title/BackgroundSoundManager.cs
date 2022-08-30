using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip backGroundAudioClip;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = backGroundAudioClip;
        audioSource.Play();
    }

    public void DeActiveSound()
    {
        audioSource.Stop();
    }
}
