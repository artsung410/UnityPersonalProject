using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

enum PasswordKeys
{
    clear = 10,
    delete
}

public class PasswordController : MonoBehaviour
{
    public static event Action PasswordUnlock = delegate { };

    [SerializeField] private TextMeshPro ScreenText;
    [SerializeField] private GameObject GreenLight;
    [SerializeField] private GameObject RedLight;


    public static bool IsGameWin;
    private string CorrectNum;
    private AudioSource audioSource;
    
    private void Awake()
    {
        IsGameWin = false;
        CorrectNum = "4617460";
        PasswordKey.KeypadSignal += ShowPasswordOnScreen;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ScreenText.text = "0";
    }

    string keyStr = "";
    private void ShowPasswordOnScreen(int key)
    {
        if (false == IsGameWin)
        {
            if (key == (int)PasswordKeys.clear)
            {
                ScreenText.text = "0";
            }

            else if (key == (int)PasswordKeys.delete)
            {
                if (ScreenText.text.Length == 1)
                {
                    ScreenText.text = "0";
                    return;
                }

                if (keyStr == "0")
                {
                    return;
                }
                else
                {
                    ScreenText.text = ScreenText.text.Substring(0, ScreenText.text.Length - 1);
                }
            }

            else
            {
                keyStr = key.ToString();

                if (ScreenText.text == "0")
                {
                    ScreenText.text = "";
                    ScreenText.text += keyStr;
                }
                else
                {
                    ScreenText.text += keyStr;
                }
            }

            if (ScreenText.text == CorrectNum)
            {
                IsGameWin = true;
                PasswordUnlock();
                SoundManager.Instance.PlayObjectSound(audioSource, "KeyPadApprove");
                GreenLight.SetActive(true);
                Debug.Log("게임에서 승리하셨습니다.");
                return;
            }

            if (ScreenText.text.Length == 8)
            {
                StartCoroutine(GlowingErrorBulb());
                SoundManager.Instance.PlayObjectSound(audioSource, "keyPadError");
                ScreenText.text = "0";
                return;
            }
        }
    }

    float lightingDuration = 1f;
    IEnumerator GlowingErrorBulb()
    {
        RedLight.SetActive(true);
        yield return new WaitForSeconds(lightingDuration);
        RedLight.SetActive(false);
    }
}
