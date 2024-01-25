using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 2;
        Value = 50;
        Desc = "A staff carved from high quality wood, and imbued with a shiny mana crystal at the top";
        Title = GetType().Name;
        ItemID = 101;
    }
}
