using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    protected override void Start()
    {
        base.Start();
        Value = 50;
        Desc = "A shiny gold key! Maybe it unlocks something of value?";
        Title = GetType().Name;
        ItemID = 200;
    }

}
