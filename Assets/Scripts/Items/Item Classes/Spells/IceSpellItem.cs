using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpellItem : SpellItem
{
    protected override void Start()
    {
        base.Start();
        itemID = 2;
        desc = "Shoots icicle projectiles that slow enemies on contact.";
        title = "Icicle Bullet";
        itemID = 2;
    }
}
