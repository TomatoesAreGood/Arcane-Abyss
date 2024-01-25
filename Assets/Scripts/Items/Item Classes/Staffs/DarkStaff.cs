using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 6;
        Value = 500;
        Desc = "A dark staff of mysterious origin. It emanates with powerful magic.";
        Title = GetType().Name;
        ItemID = 102;
    }
}
