using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpellItem : SpellItem
{
    protected override void Start()
    {
        base.Start();
        ItemID = 1;
        Desc = "Your standard fireball, inflicts burning damage over time";
        Title = "Fire Ball";
        ItemID = 1;
    }
}
