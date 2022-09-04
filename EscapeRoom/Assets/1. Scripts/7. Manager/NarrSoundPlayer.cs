using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NarrSoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] narrAudioClips;
    public AudioClip[] narrAudioClips_ending;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
    }

    public void DeActiveSound()
    {
        audioSource.Stop();
    }

    public void PlaySound(int index)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = narrAudioClips[index];
            audioSource.Play();
        }
        else
        {
            audioSource.volume = 1f;
            audioSource.clip = narrAudioClips_ending[index];
            audioSource.Play();
        }
    }

    public float GetAudioPlayTime(int index)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            return narrAudioClips[index].length;

        }
        else
        {
            return narrAudioClips_ending[index].length;
        }
    }
}
