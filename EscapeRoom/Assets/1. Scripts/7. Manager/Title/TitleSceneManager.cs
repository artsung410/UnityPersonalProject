using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [Header("FadeImage")]
    [SerializeField] private GameObject FadeImage;

    [Header("0. ManualUI")]
    [SerializeField] private GameObject ManualUI;

    [Header("1. GameStartUI")]
    [SerializeField] private GameObject GameStartUI;


    [Header("2. FlikerImage")]
    [SerializeField] private Image FlikerImage;
    [SerializeField] private AnimationCurve curveScreen;


    [Header("3. SkipButtonUI")]
    [SerializeField] private GameObject SkipButtonUI;


    // 백그라운드 이미지 / 자막
    [Header("4. BackgroundImage")]
    [SerializeField] private Sprite[] backGroundSprite;
    [SerializeField] private Image BackgroundImage;

    [Header("5. BGM_Player >>")]
    [SerializeField] private BGMPlayer bgmPlayer;

    [Header("6. Narr_Player >>")]
    [SerializeField] private NarrSoundPlayer narrPlayer;

    [Header("8. Effective_Player >>")]
    [SerializeField] private EffectiveSoundPlayer effectPlayer;

    [Header("9. Subtitle")]
    [SerializeField] private GameObject SubtitleUI;
    [SerializeField] private TextMeshProUGUI subtitleText_Eng;
    [SerializeField] private TextMeshProUGUI subtitleText_Kor;
    public string[] subtitles;
    public string[] subtitles_Kor;

    // SystemSound
    private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(FillControl());
        StartCoroutine(ShowMovie());
        StartCoroutine(PlayBackgroundMusic());
    }

    IEnumerator FillControl()
    {
        Color color = FlikerImage.color;
        while (true)
        {
            int randNum = Random.Range(1, 10);
            float randValue = 0;
            float flikerValue = Random.Range(0f, 0.3f);

            if (randNum == 2)
            {
                randValue = Random.Range(0f, 1f);
            }

            color.a = randValue;
            FlikerImage.color = color;
            yield return new WaitForSeconds(flikerValue);
        }
    }

    public void SwichingScene()
    {
        StartCoroutine(StartFadeOut());
        StartCoroutine(DelaySwitchingScene());
    }

    IEnumerator DelaySwitchingScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    public void ActiveGamestartUI()
    {
        GameStartUI.SetActive(true);
    }

    public void DeActiveSikpButtonUI()
    {
        SkipButtonUI.SetActive(false);
    }

    public void DeActiveSubtitles()
    {
        SubtitleUI.SetActive(false);
        narrPlayer.gameObject.SetActive(false);
    }

    public void DeActiveMovieImage()
    {
        BackgroundImage.gameObject.SetActive(false);
    }

    // ################# 백그라운드 이미지 / 자막 / 음성 ################# 
    int count = 0;

    IEnumerator ShowMovie()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (count == backGroundSprite.Length)
            {
                ActiveGamestartUI();
                DeActiveSikpButtonUI();
                DeActiveSubtitles();
                DeActiveImage();
                yield break;
            }

            subtitleText_Eng.text = subtitles[count];
            subtitleText_Kor.text = subtitles_Kor[count];
            BackgroundImage.sprite = backGroundSprite[count];
            if (count == 8)
            {
                effectPlayer.PlaySound();
            }

            narrPlayer.PlaySound(count);
            float duration = narrPlayer.GetAudioPlayTime(count);
            yield return new WaitForSeconds(duration);
            count++;
        }
    }

    public void PlayButtonClickSound()
    {
        audioSource.clip = buttonClickSound;
        audioSource.Play();
    }

    IEnumerator PlayBackgroundMusic()
    {
        bgmPlayer.PlaySound();
        yield return null;
    }

    IEnumerator StartFadeOut()
    {
        float fadeCount = 0;
        Image image = FadeImage.GetComponent<Image>();
        FadeImage.SetActive(true);
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    }

    public void DeActiveImage()
    {
        BackgroundImage.gameObject.SetActive(false);
    }

    public void ActivateManualUI()
    {
        ManualUI.SetActive(true);
    }

    public void DeActivateManualUI()
    {
        ManualUI.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
