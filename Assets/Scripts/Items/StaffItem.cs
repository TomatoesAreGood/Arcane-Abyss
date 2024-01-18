using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffItem : ReferencedItem
{
    protected override void Start(){
        base.Start();
        if (reference.GetComponent<Staff>() == null){
            throw new ArgumentException("object reference is not a staff");
        }
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
