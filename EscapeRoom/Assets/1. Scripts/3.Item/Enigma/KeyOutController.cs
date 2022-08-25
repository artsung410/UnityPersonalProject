using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOutController : MonoBehaviour
{
    private void Start()
    {
        // ���̵� ��
        KeyIn.KeyChangeSignal += GlowingKey;
    }

    private void GlowingKey(int key)
    {
        int childId = key;
        GameObject currentKey = transform.GetChild(childId).gameObject;
        
        currentKey.SetActive(true);
        StartCoroutine(DeActiveGlowing(currentKey));
    }


    private float keyActiveTime = 0.2f;

    private IEnumerator DeActiveGlowing(GameObject currentKey)
    {
        yield return new WaitForSeconds(keyActiveTime);
        currentKey.SetActive(false);
    }
}
