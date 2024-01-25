using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : PotionItem
{
    public override void Use(){
        PlayerController.Instance.AddMana(50);
        Destroy(gameObject);
        base.Use();
    }
    protected override void Start()
    {
        base.Start();
        Value = 25;
        Desc = "A extract of magical power which restores mana. Restores 50 mana";
        Title = GetType().Name;
        ItemID = 11;
    }
}
