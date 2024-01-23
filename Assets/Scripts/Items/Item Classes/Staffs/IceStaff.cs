using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 3;
        value = 85;
        desc = "An eloquently carved staff of ice. The crystal at the top shines with power";
        title = GetType().Name;
        itemID = 15;
    }
}
