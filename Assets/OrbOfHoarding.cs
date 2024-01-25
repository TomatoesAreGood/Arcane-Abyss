using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOfHoarding : UsableItem
{
    public override void Use()
    {
        PlayerController.Instance.Coins *= 2;
        Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        Value = 300;
        Desc = "The gods see your Greed and reward you for it";
        Title = GetType().Name;
        ItemID = 201;
    }



}
