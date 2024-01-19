using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkStaff : StaffItem
{
    protected override void Awake()
    {
        base.Awake();
        damageBonus = 3;
    }    
}
