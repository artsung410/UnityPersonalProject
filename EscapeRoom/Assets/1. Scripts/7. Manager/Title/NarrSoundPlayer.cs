using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrSoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] narrAudioClips;

    public Dictionary<int, float> audioTimeDic;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        audioTimeDic = new Dictionary<int, float>();

        for (int i = 0; i < 12; i++)
        {
            audioTimeDic.Add(i, narrAudioClips[i].length);
        }
    }

    public void DeActiveSound()
    {
        audioSource.Stop();
    }

    public void PlaySound(int index)
    {
        audioSource.clip = narrAudioClips[index];
        audioSource.Play();
    }
    public float GetAudioPlayTime(int index)
    {
        return audioTimeDic[index];
    }
}
