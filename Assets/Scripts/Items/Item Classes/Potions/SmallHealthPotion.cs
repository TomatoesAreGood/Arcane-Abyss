using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHealthPotion : PotionItem
{
    public override void Use(){
        PlayerController.Instance.GainHeart();
        Destroy(gameObject);
        base.Use();
    }
    protected override void Start()
    {
        base.Start();
        Value = 10;
        Desc = "Contains a vibrant, red liquid in a vial. Heals 1 heart.";
        Title = GetType().Name;
        ItemID = 12;
    }
}
