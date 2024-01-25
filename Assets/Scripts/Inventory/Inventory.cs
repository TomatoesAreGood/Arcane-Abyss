using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Inventory 
{  
    public Item[] Items;
    public SpellItem[] Spells;
    public PotionItem[] Potions;

    public StaffItem EquippedStaff;
    public SpellItem[] EquippedSpells;

    public Inventory(int inventorySize, int spellSize, int potionBagSize){
        Items = new Item[inventorySize];
        Spells = new SpellItem[spellSize];
        Potions = new PotionItem[potionBagSize];
        EquippedSpells = new SpellItem[4];
        // for(int i = 0; i < items.Length; i++){
        //     items[i] = null;
        // }
        // for(int i = 0; i < equippedSpells.Length; i++){
        //     equippedSpells[i] = null;
        // }

    }


}