using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotSpellBook : SpellBook
{
    public override void Use()
    {
        if(PickUpController.instance.TryAddSpell(ItemLibrary.instance.fireShot)){
            Destroy(gameObject);
        }
    }
    protected override void Start()
    {
        base.Start();
        value = 200;
        desc = "Use to learn FireBall spell";
        title = GetType().Name;
        itemID = 20;
    }
}
