using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpellItem : SpellItem
{
    protected override void Start()
    {
        base.Start();
        ItemID = 2;
        Desc = "Shoots icicle projectiles that slow enemies on contact.";
        Title = "Icicle Spear";
        ItemID = 2;
    }
}
