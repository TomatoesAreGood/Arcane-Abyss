using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    protected override void Start()
    {
        base.Start();
        value = 50;
        desc = "A shiny gold key! Maybe it unlocks something of value?";
        title = GetType().Name;
        itemID = 20;
    }

}
