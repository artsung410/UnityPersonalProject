using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum Scene
{
    title,
    main,
    ending,
}

public class GameManager : SingletonBehaviour<GameManager>
{
    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("게임종료");
    }

    public void LoadTitleScene()
    {
        LoadScene((int)Scene.title);
    }

    public void LoadScene(int SceneNum)
    {
        StartCoroutine(DelayLoadScene(SceneNum));
    }

    IEnumerator DelayLoadScene(int SceneNum)
    {
        StartCoroutine(StartFadeOut());
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneNum);
    }

    IEnumerator StartFadeOut()
    {
        float fadeCount = 0;

        PlayerHUD.Instance.FadeImage.SetActive(true);
        Image image = PlayerHUD.Instance.FadeImage.GetComponent<Image>();

        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    }
}
