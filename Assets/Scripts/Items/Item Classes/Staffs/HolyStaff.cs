using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 4;
        Value = 100;
        Desc = "A divine staff that has wings sprouting from its sides";
        Title = GetType().Name;
        ItemID = 105;
    }
}
