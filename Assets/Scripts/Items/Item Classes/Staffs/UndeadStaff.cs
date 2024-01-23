using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 3;
        value = 85;
        desc = "An ominous looking staff topped with a skull";
        title = GetType().Name;
        itemID = 19;
    }
}
