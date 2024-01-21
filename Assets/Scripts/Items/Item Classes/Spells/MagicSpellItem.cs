using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpellItem : SpellItem
{
    protected override void Start()
    {
        base.Start();
        itemID = 3;
        desc = "The standard magic spell. Inflcits minor damage, no special effects";
        title = "Magic Missile";
    }
}
