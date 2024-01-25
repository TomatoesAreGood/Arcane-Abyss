using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 3;
        Value = 85;
        Desc = "An eloquently carved staff of ice. The crystal at the top shines with power";
        Title = GetType().Name;
        ItemID = 103;
    }
}
