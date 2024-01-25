using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class SpellItem : ReferencedItem{
    protected override void Awake(){
        base.Awake();
        renderer = PlayerController.Instance.inventoryUI.spellsRenderer;
        inventory = PlayerController.Instance.inventory.spells;
    }
    protected override void Start(){
        base.Start();
         if (reference == null){
            throw new ArgumentException("no object reference attached to item");
        }   
        if (reference.GetComponent<Spell>() == null){
            throw new ArgumentException("object reference is not a spell");
        }
    }


    public override void EquipSpellSlot1(){
        PlayerController.Instance.EquipSpell(this, 0);
    }
    public override void EquipSpellSlot2(){
        PlayerController.Instance.EquipSpell(this, 1);        


    } 
    public override void EquipSpellSlot3(){
        PlayerController.Instance.EquipSpell(this, 2);       


    } 
    public override void EquipSpellSlot4(){
        PlayerController.Instance.EquipSpell(this, 3);        


    }

}