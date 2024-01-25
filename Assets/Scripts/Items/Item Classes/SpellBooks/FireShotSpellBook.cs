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
        Value = 200;
        Desc = "Use to learn FireBall spell";
        Title = GetType().Name;
        ItemID = 20;
    }
}
