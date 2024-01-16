using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Inventory 
{  
    public Item[] items;
    public SpellItem[] spells;
    public Potion[] potions;

    public StaffItem equippedStaff;
    public SpellItem[] equippedSpells;

    public int size;

    public Inventory(int inventorySize, int spellSize, int potionBagSize){
        items = new Item[inventorySize];
        spells = new SpellItem[spellSize];
        potions = new Potion[potionBagSize];
        size = items.Length;
        for(int i = 0; i < size; i++){
            items[i] = null;
        }
    }


}