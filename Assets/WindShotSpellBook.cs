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
        value = 200;
        desc = "Use to learn Wind Blade spell";
        title = GetType().Name;
        itemID = 14;
    }
}
