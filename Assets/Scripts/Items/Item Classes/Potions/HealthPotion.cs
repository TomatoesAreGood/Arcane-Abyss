using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PotionItem
{
    public override void Use(){
        PlayerController.instance.GainHeart(2);
        Destroy(gameObject);
    }

}
