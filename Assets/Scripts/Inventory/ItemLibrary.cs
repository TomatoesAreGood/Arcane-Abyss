using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using static UnityEditor.Progress;
using System;


public class ItemLibrary : MonoBehaviour
{
    public object[] itemsArray;
    public Item[] Library;

    public static ItemLibrary instance;
    
    public SpellItem fireball;
    public SpellItem iceShot;
    public SpellItem magicShot;
    public SpellItem windShot;
    public StaffItem basicStaff;
    public StaffItem forestStaff;
    public PotionItem healthPotion;
    public StaffItem darkstaff;
    public SpellBook fireShotSpellBook;
    public PotionItem smallHealthPot;


    // Start is called before the first frame update
    private void Awake(){
        if(instance == null){
            instance = this;
            Library = new Item[] { basicStaff, forestStaff, fireball, magicShot, iceShot, windShot, healthPotion, darkstaff, fireShotSpellBook, smallHealthPot };

            InitalizeItemToArray();


        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitalizeItemToArray()
    {
        List<object> items = new List<object>();
        foreach (FieldInfo field in typeof(ItemLibrary).GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            object value = field.GetValue(this);
            if (value != null)
            {
                items.Add(value);
            }
        }
        itemsArray = items.ToArray();

        foreach (var item in itemsArray)
        {
            //Debug.Log(item.GetType().Name + ": " + item.ToString());
       }
    }

    public Item GetItemReference(Item item){
        if(item.GetType() == typeof(BasicStaff)){
            return basicStaff;
        }else if(item.GetType() == typeof(ForestStaff)){
            return forestStaff;
        }else if(item.GetType() == typeof(FireSpellItem)){
            return fireball;
        }else if(item.GetType() == typeof(IceSpellItem)){
            return iceShot;
        }else if (item.GetType() == typeof(DarkStaff)){
            return darkstaff;
        }else if (item.GetType() == typeof(MagicSpellItem)){
            return magicShot;
        }else if (item.GetType() == typeof(HealthPotion)){
            return healthPotion;
        }else if (item.GetType() == typeof(FireShotSpellBook)){
            return fireShotSpellBook;
        }else if (item.GetType() == typeof(WindSpellItem)){
            return windShot;
        }else if (item.GetType() == typeof(SmallHealthPotion)){
            return smallHealthPot;
        }
        throw new ArgumentException("Could not find item reference");
    }

    
}
