using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 5;
        value = 300;
        desc = "A three-pronged staff that releases three times more magic";
        title = GetType().Name;
        itemID = 18;
    }
}
