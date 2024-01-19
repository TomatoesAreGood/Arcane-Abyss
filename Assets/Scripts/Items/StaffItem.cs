using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffItem : Item
{
    public float damageBonus;

    protected override void Awake()
    {
        base.Awake();
        damageBonus = 0;
    }

    public override void Equip(){
        PlayerController.instance.EquipStaff(this);
    }

    public override void Drop()
    {
        GameObject obj = Instantiate(PickUpController.instance.defaultDropItem);
        obj.transform.position = PlayerController.characterPos;
        obj.GetComponent<PickupScript>().itemReference = ItemLibrary.instance.GetItemReference(this);
        if(this == PlayerController.instance.inventory.equippedStaff){
            PlayerController.instance.inventory.equippedStaff = null;
        }
        Destroy(gameObject);
    }


}
