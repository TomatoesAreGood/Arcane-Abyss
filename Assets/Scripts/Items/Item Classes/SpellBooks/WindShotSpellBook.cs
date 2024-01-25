using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindShotSpellBook : SpellBook
{
    public override void Use()
    {
        if (PickUpController.instance.TryAddSpell(ItemLibrary.instance.windShot))
        {
            Destroy(gameObject);
        }
    }
    protected override void Start()
    {
        base.Start();
        Value = 200;
        Desc = "Use to learn Wind Blade spell";
        Title = GetType().Name;
        ItemID = 22;
    }
}
