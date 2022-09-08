using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOutController : MonoBehaviour
{
    private void Start()
    {
        KeyIn.KeyChangeSignal += GlowingKey;
    }

    float _keyActiveTime = 0.2f;
    private void GlowingKey(int key)
    {
        int childId = key;
        GameObject currentKey = transform.GetChild(childId).gameObject;
        currentKey.SetActive(true);
        StartCoroutine(DeActiveGlowing(currentKey));
    }

    private IEnumerator DeActiveGlowing(GameObject currentKey)
    {
        yield return new WaitForSeconds(_keyActiveTime);
        currentKey.SetActive(false);

    }
}
