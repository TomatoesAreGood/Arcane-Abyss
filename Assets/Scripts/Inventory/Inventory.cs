using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Inventory 
{  
    public Item[] items;
    public SpellItem[] spells;
    public PotionItem[] potions;

    public StaffItem equippedStaff;
    public SpellItem[] equippedSpells;

    public Inventory(int inventorySize, int spellSize, int potionBagSize){
        items = new Item[inventorySize];
        spells = new SpellItem[spellSize];
        potions = new PotionItem[potionBagSize];
        equippedSpells = new SpellItem[4];
        // for(int i = 0; i < items.Length; i++){
        //     items[i] = null;
        // }
        // for(int i = 0; i < equippedSpells.Length; i++){
        //     equippedSpells[i] = null;
        // }

    }


}