using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInController : MonoBehaviour
{
    public float pingpongSpeed = 0.5f;
    private bool IsPushingKey = false;
    Vector3 PrevPosition;
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
        PrevPosition = currentKey.transform.position;
        StartCoroutine(pushingKey(currentKey));
        StartCoroutine(DeactivePushing(currentKey));
    }

    private IEnumerator pushingKey(GameObject currentKey)
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
                currentKey.transform.position = PrevPosition;
                yield break;
            }
        }
    }

    private IEnumerator DeactivePushing(GameObject currentKey)
    {
        yield return new WaitForSeconds(0.2f);
        IsPushingKey = false;
    }
}
