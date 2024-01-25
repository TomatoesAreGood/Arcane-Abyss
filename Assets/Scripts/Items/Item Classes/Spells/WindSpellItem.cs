using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpellItem : SpellItem
{
    protected override void Start()
    {
        base.Start();
        ItemID = 4;
        Desc = "Summons a powerful gust of wind which stops enemies from moving.";
        Title = "Wind Blade";

    }
}
