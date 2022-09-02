using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitDoor : InterectiveObject
{
    bool isExitDoorOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void Operate()
    {
        if (true == PasswordController.IsGameWin && false == isExitDoorOpen)
        {
            isActive = true;
            isExitDoorOpen = true;
            SoundManager.Instance.PlayObjectSound(audioSource, "ExitDoorOpen");
            animator.SetBool("isActive", true);
            PlayerHUD.Instance.EscapeSuccessUI.SetActive(true);
            StartCoroutine(JumpToEndingScene());
        }
    }

    IEnumerator JumpToEndingScene()
    {
        StartCoroutine(StartFadeOut());
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
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
