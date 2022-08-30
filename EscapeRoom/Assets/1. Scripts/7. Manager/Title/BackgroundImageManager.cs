using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackgroundImageManager : MonoBehaviour
{
    [SerializeField] private Sprite[] backGroundSprite;
    [SerializeField] Image BackgroundImage;
    [SerializeField] TextMeshProUGUI subtitleText;

    public string[] subtitle;

    private void Start()
    {
        StartCoroutine(ShowImage());
    }

    IEnumerator ShowImage()
    {
        int count = 0;
        while(true)
        {
            if (count == backGroundSprite.Length)
            {
                TitleSceneManager.Instance.ActiveGamestartUI();
                TitleSceneManager.Instance.DeActiveSikpButtonUI();
                TitleSceneManager.Instance.DeActiveSubtitles();
                DeActiveImage();
                yield break;
            }

            subtitleText.text = subtitle[count];
            BackgroundImage.sprite = backGroundSprite[count];
            yield return new WaitForSeconds(4.5f);
            count++;
        }
    }

    public void DeActiveImage()
    {
        BackgroundImage.gameObject.SetActive(false);
    }

}
