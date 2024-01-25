using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallManaPotion : PotionItem
{
    public override void Use(){
        PlayerController.Instance.AddMana(25f);
        Destroy(gameObject);
        base.Use();
    }
    protected override void Start()
    {
        base.Start();
        Value = 15;
        Desc = "Contains a cool-looking blue liquid in a vial. Regenerates 25 mana.";
        Title = GetType().Name;
        ItemID = 13;
    }
}
