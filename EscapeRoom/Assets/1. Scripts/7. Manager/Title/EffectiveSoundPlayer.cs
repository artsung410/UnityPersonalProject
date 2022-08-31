using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectiveSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip dogSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.clip = dogSound;
        audioSource.Play();
    }
}
