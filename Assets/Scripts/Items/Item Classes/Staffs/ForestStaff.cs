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
        desc = "";
        title = GetType().Name;
    }
}
