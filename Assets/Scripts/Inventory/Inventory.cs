using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory 
{  
    public Item[,] itemGrid;
    public SpellItem[] spells;
    public StaffItem equippedStaff;

    public Inventory(){
        itemGrid = new Item[PlayerController.instance.inventorySize.Item1, PlayerController.instance.inventorySize.Item2];
        spells = new SpellItem[PlayerController.instance.spellSlots];
    }


}