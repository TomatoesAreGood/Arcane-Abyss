using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallManaPotion : PotionItem
{
    public override void Use(){
        PlayerController.instance.AddMana(25f);
        Destroy(gameObject);
        base.Use();
    }
    protected override void Start()
    {
        base.Start();
        value = 15;
        desc = "Contains a cool-looking blue liquid in a vial. Regenerates 25 mana.";
        title = GetType().Name;
        itemID = 11;
    }
}
