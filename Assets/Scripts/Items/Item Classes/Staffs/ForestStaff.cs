using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 2;
        value = 50;
        desc = "A staff carved from high quality wood, and imbued with a shiny mana crystal at the top";
        title = GetType().Name;
        itemID = 101;
    }
}
