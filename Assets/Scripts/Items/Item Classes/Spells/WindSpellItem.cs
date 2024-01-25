using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpellItem : SpellItem
{
    protected override void Start()
    {
        base.Start();
        itemID = 4;
        desc = "Summons a powerful gust of wind which stops enemies from moving.";
        title = "Wind Blade";

    }
}
