using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum PasswordKeys
{
    clear = 10,
    delete
}

public class PasswordController : MonoBehaviour
{
    [SerializeField] private TextMeshPro ScreenText;

    public static bool IsGameWin;
    private string CorrectNum;

    private void Awake()
    {
        IsGameWin = false;
        CorrectNum = "4617460";
        PasswordKey.KeypadSignal += ShowPasswordOnScreen;
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
                Debug.Log("게임에서 승리하셨습니다.");
                return;
            }

            if (ScreenText.text.Length == 8)
            {
                Debug.Log("asdfasdf");
                ScreenText.text = "0";
                return;

                // 금지 사운드 추가
            }
        }
    }
}
