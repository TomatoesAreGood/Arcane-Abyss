using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStaff : StaffItem
{
    protected override void Awake()
    {
        base.Awake();
        damageBonus = 1;
    }
}