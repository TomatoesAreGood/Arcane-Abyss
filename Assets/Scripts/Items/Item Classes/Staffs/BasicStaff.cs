using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 1;
        Value = 10;
        Desc = "A pretty simple looking staff which mildly enhances spell power";
        Title = GetType().Name;
        ItemID = 100;
    }
}