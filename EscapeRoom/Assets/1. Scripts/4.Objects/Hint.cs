using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : InterectiveObject
{
    private int _ID;
    public int ID { get { return _ID; } set { _ID = value; }}

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public override void Operate()
    {
        PlayerHUD.Instance.ActiveHintImage(ID);
        SoundManager.Instance.PlayObjectSound(audioSource, "Carpet");
    }
}
