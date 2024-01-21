using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 3;
        value = 100;
        desc = "";
        title = GetType().Name;
    }
}
