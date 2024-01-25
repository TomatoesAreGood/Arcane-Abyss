using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 5;
        Value = 300;
        Desc = "A three-pronged staff that releases three times more magic";
        Title = GetType().Name;
        ItemID = 106;
    }
}
