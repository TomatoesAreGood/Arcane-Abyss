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
        value = 300;
        desc = "The God sees your greed and reward you for it";
        title = GetType().Name;
        itemID = 201;
    }



}
