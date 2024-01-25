using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PotionItem
{
    public override void Use(){
        PlayerController.Instance.GainHeart(2);
        Destroy(gameObject);
        base.Use();
    }
     protected override void Start()
    {
        base.Start();
        value = 20;
        desc = "A red-colored potion which restores vitality. Heal 2 hearts.";
        title = GetType().Name;
        itemID = 10;
    }
    
}
