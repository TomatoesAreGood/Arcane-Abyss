using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{  
    public static Inventory instance;
    private Item[,] itemGrid;
    public SpellItem[] spells;
    public StaffItem equippedStaff;


    private void Start()
    {
        if (instance == null){
            instance = this;
        }else{
            Destroy(this);
        }

        itemGrid = new Item[PlayerController.instance.inventorySize.Item1, PlayerController.instance.inventorySize.Item2];
        spells = new SpellItem[PlayerController.instance.spellSlots];
    }


}