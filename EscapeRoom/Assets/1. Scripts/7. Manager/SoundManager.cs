using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Sound
{
    BoxMove,
    BoxReset,
    DrawerOpen,
    DrawerClose,
    Carpet,
    RotateLock,
    Unlock,
    ToolBoxOpen,
    ToolBoxClose,
    PushKey,
    keyPadError,
    KeyPadApprove,
    BookCaseOpen,
    IronDoorOpen,
    IronDoorClose,
    WoddenDoorOpen,
    WoddenDoorClose,
    ExitDoorOpen,
    Locked,
    KeyPush,
    Lench,
    End,
}


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;

    [Header("<<SystemSound>>")]
    [Header("ItemSound")]
    [SerializeField] private AudioClip PickupSound;

    [Header("UISound")]
    [SerializeField] private AudioClip ItemInfoSound;
    [SerializeField] private AudioClip ButtonClickSound;

    [Header("<<InteractiveObejctSound>>")]
    [SerializeField] private AudioClip[] audioClips;

    Sound sound;
    Dictionary<string, AudioClip> AudioDic;
    // SoundManager.Instance.PlayItemInfoSound()
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        AudioDic = new Dictionary<string, AudioClip>();
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < (int)Sound.End; i++)
        {
            sound = (Sound)i;
            AudioDic.Add(sound.ToString(), audioClips[i]);
        }
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

    public void PlayObjectSound(AudioSource source, string name)
    {
        source.Stop();
        source.clip = AudioDic[name];
        source.Play();
    }

    public void PlayObjectSound(AudioSource source, string name1, string name2, bool isActive)
    {
        source.Stop();
        source.clip = isActive == true ? AudioDic[name1] : AudioDic[name2];
        source.Play();
    }

    public void PlayObjectSound(AudioSource source, string name1, string name2, string name3, bool isActive)
    {
        source.Stop();
        source.clip = isActive == true ? AudioDic[name1] : AudioDic[name2];
        source.Play();
    }
}
