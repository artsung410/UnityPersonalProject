using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    RotateUnlock,
    KeyGet,
    End,
}


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource ItemAudioSource;

    [Header("<<SystemSound>>")]
    [Header("BGM")]
    [SerializeField] private GameObject BGMAudio;
    [SerializeField] private AudioClip BGM;
    private AudioSource BGMAudioSource;

    [Header("ItemSound")]
    [SerializeField] private AudioClip PickupSound;
    [SerializeField] private AudioClip EnigmaPartSwitchingSound;

    [Header("UISound")]
    [SerializeField] private AudioClip ItemInfoSound;
    [SerializeField] private AudioClip ButtonClickSound;


    [Header("<<InteractiveObejctSound>>")]
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource[] InteractiveAudioSources;

    [Header("Music/Sound Slider UI")]
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SoundSlider;

    Sound sound;
    Dictionary<string, AudioClip> AudioDic;
    // SoundManager.Instance.PlayItemInfoSound()
    private void Awake()
    {
        Instance = this;
        BGMAudioSource = BGMAudio.gameObject.AddComponent<AudioSource>();
        ItemAudioSource = GetComponent<AudioSource>();
        AudioDic = new Dictionary<string, AudioClip>();
    }

    private void Start()
    {
        // BGMÀç»ý
        Init_All_AudioSource_Volumes();
        BGMAudioSource.clip = BGM;
        BGMAudioSource.loop = true;
        BGMAudioSource.Play();

        for (int i = 0; i < (int)Sound.End; i++)
        {
            sound = (Sound)i;
            AudioDic.Add(sound.ToString(), audioClips[i]);
        }
    }

    private void Init_All_AudioSource_Volumes()
    {
        BGMAudioSource.volume = 0.75f;
        ItemAudioSource.volume = 0.75f;

        for(int i = 0; i < InteractiveAudioSources.Length; i++)
        {
            InteractiveAudioSources[i].volume = 0.75f;
        }

    }

    public void playPickupSound(Item item)
    {
        if (item.itemName == "Silver_Key" || item.itemName == "Gold_Key")
        {
            ItemAudioSource.clip = AudioDic["KeyGet"];
        }
        else
        {
            ItemAudioSource.clip = PickupSound;
        }

        ItemAudioSource.Play();
    }

    public void playItemInfoSound()
    {
        ItemAudioSource.clip = ItemInfoSound;
        ItemAudioSource.Play();
    }

    public void PlayButtonClickSound()
    {
        ItemAudioSource.clip = ButtonClickSound;
        ItemAudioSource.Play();
    }

    public void PlayEnigmaSwitchingSound()
    {
        ItemAudioSource.clip = EnigmaPartSwitchingSound;
        ItemAudioSource.Play();
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

    public void On_BGM_SliderEvent(float volume)
    {
        BGMAudioSource.volume = volume;
    }

    public void On_Sound_SliderEvent(float volume)
    {
        ItemAudioSource.volume = volume;

        for (int i = 0; i < InteractiveAudioSources.Length; i++)
        {
            InteractiveAudioSources[i].volume = volume;
        }
    }
    
    public void Reset_Slider()
    {
        MusicSlider.value = 0.75f;
        SoundSlider.value = 0.75f;
    }

}
