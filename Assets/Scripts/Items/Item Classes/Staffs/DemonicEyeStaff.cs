using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DemonicEyeStaff : StaffItem
{
    protected override void Start()
    {
        base.Start();
        damageBonus = 4;
        Value = 100;
        Desc = "A blood-red staff topped with a large pulsating eye that exudes magic";
        Title = GetType().Name;
        ItemID = 104;
    }
}
