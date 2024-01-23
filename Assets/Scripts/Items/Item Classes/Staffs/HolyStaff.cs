using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 4;
        value = 100;
        desc = "A divine staff that has wings sprouting from its sides";
        title = GetType().Name;
        itemID = 17;
    }
}
