using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHealthPotionItem : PotionItem
{
    public override void Use(){
        PlayerController.instance.IncreaseMaxHealth();
        Destroy(gameObject);
    }

}
