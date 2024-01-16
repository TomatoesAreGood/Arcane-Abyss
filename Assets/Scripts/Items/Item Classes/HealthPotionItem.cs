using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionItem : PotionItem
{
    public override void Use(){
        PlayerController.instance.IncreaseMaxHealth(2);
        Destroy(gameObject);
    }

}
