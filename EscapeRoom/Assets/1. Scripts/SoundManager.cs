using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip PickupSound;
    [SerializeField] private AudioClip ItemInfoSound;
    [SerializeField] private AudioClip ButtonClickSound;

    // SoundManager.Instance.PlayItemInfoSound()
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void playPickupSound()
    {
        audioSource.clip = PickupSound;
        audioSource.Play();
    }

    public void playItemInfoSound()
    {
        audioSource.clip = ItemInfoSound;
        audioSource.Play();
    }

    public void PlayButtonClickSound()
    {
        audioSource.clip = ButtonClickSound;
        audioSource.Play();
    }

    //public void PlayInteractiveObjectSound(int ID)
    //{
        
    //}
}
