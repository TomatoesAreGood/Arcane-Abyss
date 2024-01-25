using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpellItem : SpellItem
{
    protected override void Start()
    {
        base.Start();
        ItemID = 3;
        Desc = "The standard magic spell. Inflcits minor damage, no special effects";
        Title = "Magic Missile";
    }
}
