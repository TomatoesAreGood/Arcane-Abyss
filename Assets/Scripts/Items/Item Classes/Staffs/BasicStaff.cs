using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 1;
        value = 10;
        desc = "";
        title = GetType().Name;
    }
}