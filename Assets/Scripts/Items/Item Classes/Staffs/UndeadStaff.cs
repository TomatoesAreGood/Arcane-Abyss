using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 3;
        Value = 85;
        Desc = "An ominous looking staff topped with a skull";
        Title = GetType().Name;
        ItemID = 107;
    }
}
