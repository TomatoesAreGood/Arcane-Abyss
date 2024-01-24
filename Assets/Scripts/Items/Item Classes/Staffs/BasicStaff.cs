using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 1;
        value = 10;
        desc = "A pretty simple looking staff which mildly enhances spell power";
        title = GetType().Name;
        itemID = 100;
    }
}