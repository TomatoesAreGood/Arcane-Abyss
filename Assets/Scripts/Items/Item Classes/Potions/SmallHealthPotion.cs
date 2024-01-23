using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHealthPotion : PotionItem
{
    public override void Use(){
        PlayerController.instance.GainHeart();
        Destroy(gameObject);
        base.Use();
    }
    protected override void Start()
    {
        base.Start();
        value = 10;
        desc = "Contains a vibrant, red liquid in a vial. Heals 1 heart.";
        title = GetType().Name;
        itemID = 10;
    }
}
