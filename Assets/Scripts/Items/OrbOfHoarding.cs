using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOfHoarding : Item
{
    protected override void Start()
    {
        base.Start();
        value = 1000;
        desc = "Your greed doesn't go unoticed. The Gods give you more fortune!";
        title = GetType().Name;
        itemID = 205;
    }
}
