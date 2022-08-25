using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInController : MonoBehaviour
{
    public float pingpongSpeed = 0.5f;
    private bool IsPushingKey = false;
    Vector3 CurrentPosition;
    Vector3[] PrevPosition = new Vector3[26];
    bool[] IsPrevPositionsSave = new bool[26];

    private void Start()
    {
        KeyIn.KeySignal += GetSignal;
    }

    private void GetSignal(int key)
    {
        Debug.Log(key);
        int childId = key;

        Debug.Log(childId);
        GameObject currentKey = transform.GetChild(childId).gameObject;

        CurrentPosition = currentKey.transform.position;

        if (false == IsPrevPositionsSave[key])
        {
            IsPrevPositionsSave[key] = true;
            PrevPosition[key] = CurrentPosition;
        }

        StartCoroutine(pushingKey(currentKey, key));
        StartCoroutine(DeactivePushing(currentKey));
    }

    private IEnumerator pushingKey(GameObject currentKey, int key)
    {
        IsPushingKey = true;

        DeactivePushing(currentKey);
        float y = currentKey.transform.position.y;
        float z = currentKey.transform.position.z;
        Vector3 dirDown = new Vector3(0f, 1f, -4f);
        while (true)
        {
            if(true == IsPushingKey)
            {
                currentKey.transform.Translate(dirDown.normalized * Time.deltaTime * pingpongSpeed);
                yield return null;
            }

            else
            {
                currentKey.transform.position = PrevPosition[key];
                yield break;
            }
        }
    }

    private IEnumerator DeactivePushing(GameObject currentKey)
    {
        yield return new WaitForSeconds(0.5f);
        IsPushingKey = false;
    }
}
