using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpellItem : SpellItem
{
    protected override void Start()
    {
        base.Start();
        itemID = 1;
        desc = "Your standard fireball, inflicts burning damage over time";
        title = "Fire Ball";
        itemID = 1;
    }
}
