using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : InterectiveObject
{
    public int ID;

    public override void Operate()
    {
        PlayerHUD.Instance.ActiveHintImage(ID);
    }
}
