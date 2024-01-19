using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStaff : StaffItem
{
    protected override void Awake()
    {
        base.Awake();
        damageBonus = 2;
    }
}
