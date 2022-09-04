using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
    // Fade이미지
    [SerializeField] private GameObject FadeImage;

    // 백그라운드 이미지 / 자막
    [Header("4. BackgroundImage")]
    [SerializeField] private Sprite[] backGroundSprite;
    [SerializeField] private Image BackgroundImage;

    [Header("5. BGM_Player >>")]
    [SerializeField] private BGMPlayer bgmPlayer;

    [Header("6. Narr_Player >>")]
    [SerializeField] private NarrSoundPlayer narrPlayer;

    [Header("9. Subtitle")]
    [SerializeField] private GameObject SubtitleUI;
    [SerializeField] private TextMeshProUGUI subtitleText_Eng;
    [SerializeField] private TextMeshProUGUI subtitleText_Kor;

    public string[] subtitles;
    public string[] subtitles_Kor;

    public string[] subtitles_Ending;
    public string[] subtitles_Kor_Ending;

    private void Start()
    {
        StartCoroutine(StartFadeIn());
        StartCoroutine(DelayStart());
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
    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowMovie());
        StartCoroutine(PlayBackgroundMusic());
        
    }

    IEnumerator ShowMovie()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.7f);

            if (count == backGroundSprite.Length)
            {
                DeActiveSubtitles();
                DeActiveImage();
                yield break;
            }

            subtitleText_Eng.text = subtitles_Ending[count];
            subtitleText_Kor.text = subtitles_Kor_Ending[count];
            BackgroundImage.sprite = backGroundSprite[count];

            narrPlayer.PlaySound(count);
            float duration = narrPlayer.GetAudioPlayTime(count);

            if (count == 7 || count == 9)
            {
                duration += 1.5f;
            }

            yield return new WaitForSeconds(duration);
            count++;
        }
    }

    IEnumerator PlayBackgroundMusic()
    {
        bgmPlayer.PlaySound();
        yield return null;
    }

    IEnumerator StartFadeIn()
    {
        float fadeCount = 1;

        Image image = FadeImage.GetComponent<Image>();

        while (fadeCount > 0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }

        FadeImage.SetActive(false);
    }

    public void DeActiveImage()
    {
        BackgroundImage.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
