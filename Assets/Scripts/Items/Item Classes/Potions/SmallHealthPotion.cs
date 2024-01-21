using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHealthPotion : PotionItem
{
    public override void Use(){
        PlayerController.instance.GainHeart();
        Destroy(gameObject);
    }

}
