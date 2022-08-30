using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] Image backGroundImage;
    [SerializeField] private AnimationCurve curveScreen;

    private void Start()
    {
        StartCoroutine(FillControl());
    }

    IEnumerator FillControl()
    {
        Color color = backGroundImage.color;
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
            backGroundImage.color = color;
            yield return new WaitForSeconds(flikerValue);

        }
    }

    public void SwichingScene()
    {
        SceneManager.LoadScene(1);
    }
}
