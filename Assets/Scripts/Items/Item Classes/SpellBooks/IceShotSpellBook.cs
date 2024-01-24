using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShotSpellBook : SpellBook
{
    public override void Use()
    {
        if(PickUpController.instance.TryAddSpell(ItemLibrary.instance.iceShot)){
            Destroy(gameObject);
        }
    }
    protected override void Start()
    {
        base.Start();
        value = 200;
        desc = "Use to learn Icicle Spear spell";
        title = GetType().Name;
        itemID = 21;
    }
}
