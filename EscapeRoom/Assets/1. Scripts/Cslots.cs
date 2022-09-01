using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Cslots : MonoBehaviour
{
    public static event Action<Sprite> CslotsButtonClickSignal = delegate { };

    public void ButtonClick()
    {
        CslotsButtonClickSignal.Invoke(GetComponent<Image>().sprite);
    }
}
