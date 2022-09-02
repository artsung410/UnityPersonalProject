using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip backGroundAudioClip;
    [SerializeField] private AudioClip backGroundAudioClip_Ending;

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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = backGroundAudioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = backGroundAudioClip_Ending;
            audioSource.Play();
        }
    }
}
