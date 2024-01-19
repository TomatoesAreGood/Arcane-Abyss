using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStaffItem : StaffItem
{
    protected override void Awake()
    {
        base.Awake();
        damageBonus = 1;
    }
}