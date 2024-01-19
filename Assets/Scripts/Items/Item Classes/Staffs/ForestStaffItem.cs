using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStaffItem : StaffItem
{
    protected override void Awake()
    {
        base.Awake();
        damageBonus = 2;
    }
}
