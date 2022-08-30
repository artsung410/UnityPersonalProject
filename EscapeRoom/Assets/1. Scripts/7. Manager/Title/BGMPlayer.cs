using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip backGroundAudioClip;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void DeActiveSound()
    {
        audioSource.Stop();
    }

    public void PlaySound()
    {
        audioSource.clip = backGroundAudioClip;
        audioSource.Play();
    }
}
