using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 6;
        value = 500;
        desc = "A dark staff of mysterious origin. It emanates with powerful magic.";
        title = GetType().Name;
        itemID = 8;
    }
}
