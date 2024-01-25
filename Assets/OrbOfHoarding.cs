using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOfHoarding : UsableItem
{
    public override void Use()
    {
        PlayerController.instance.coins *= 2;
        Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        value = 300;
        desc = "The gods see your Greed and reward you for it";
        title = GetType().Name;
        itemID = 201;
    }



}
