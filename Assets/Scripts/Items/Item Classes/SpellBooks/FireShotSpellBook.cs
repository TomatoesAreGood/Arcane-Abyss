using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotSpellBook : SpellBook
{
    public override void Use()
    {
        if(PickUpController.instance.TryAddSpell(ItemLibrary.instance.fireball)){
            Destroy(gameObject);
        }
    }
    protected override void Start()
    {
        base.Start();
        value = 200;
        desc = "";
        title = GetType().Name;
    }
}
