using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffShop : ShopUI
{
    public StaffItem[] staffLibrary;
    public Item[] item;
    
   public override void Awake()
    {
        numItems = 5;
        shopItems = new Item[numItems];
        item = ItemLibrary.instance.Library;
        int count = 0;
        for (int i = 0; i < item.Length; i++)
        {
            
            if (item[i].itemID > 100 && item[i].itemID < 200) 
            {
                count++;
            }
            
            

        }
        staffLibrary = new StaffItem[count];
        for(int i = 0;i < item.Length; i++)
        {
            if(item[i].itemID > 100 && item[i].itemID < 200)
            {
                staffLibrary[i] = (StaffItem)item[i];
            }
        }

    }






}
