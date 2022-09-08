using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInController : MonoBehaviour
{
    private float       _pingpongSpeed      = 0.5f;
    private bool        _isPushingKey       = false;
    private Vector3     CurrentPosition;
    private Vector3[]   PrevPosition        = new Vector3[26];
    private bool[]      IsPrevPositionsSave = new bool[26];

    private void Start()
    {
        KeyIn.KeySignal += GetSignal;
    }

    private void GetSignal(int key)
    {
        int childId = key;

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
        _isPushingKey = true;

        DeactivePushing(currentKey);
        float y = currentKey.transform.position.y;
        float z = currentKey.transform.position.z;
        Vector3 dirDown = new Vector3(0f, 1f, -4f);
        while (true)
        {
            if(true == _isPushingKey)
            {
                currentKey.transform.Translate(dirDown.normalized * Time.deltaTime * _pingpongSpeed);
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
        yield return new WaitForSeconds(0.2f);
        _isPushingKey = false;
    }
}
