using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Inventory 
{  
    public Item[] items;
    public SpellItem[] spells;
    public StaffItem equippedStaff;
    public int size;

    public Inventory(int inventorySize, int spellSize){
        items = new Item[inventorySize];
        spells = new SpellItem[spellSize];
        size = items.Length;
        for(int i = 0; i < size; i++){
            items[i] = null;
        }
    }


}