using UnityEngine;
using System;

public abstract class SpellItem : ReferencedItem{

    protected override void Start(){
        base.Start();
        if (reference.GetComponent<Spell>() == null){
            throw new ArgumentException("object reference is not a spell");
        }
    }


    public override void EquipSpellSlot1(){
        PlayerController.instance.EquipSpell(this, 0);
    }
    public override void EquipSpellSlot2(){
        PlayerController.instance.EquipSpell(this, 1);        


    } 
    public override void EquipSpellSlot3(){
        PlayerController.instance.EquipSpell(this, 2);       


    } 
    public override void EquipSpellSlot4(){
        PlayerController.instance.EquipSpell(this, 3);        


    }

}