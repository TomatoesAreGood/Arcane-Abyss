using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShotSpellBook : SpellBook
{
    public override void Use()
    {
        if(PickUpController.instance.TryAddSpell(ItemLibrary.instance.iceShot)){
            SoundManager.instance.PlayPageFlipSFX();
            Destroy(gameObject);
        }
    }
    protected override void Start()
    {
        base.Start();
        Value = 200;
        Desc = "Use to learn Icicle Spear spell";
        Title = GetType().Name;
        ItemID = 21;
    }
}
