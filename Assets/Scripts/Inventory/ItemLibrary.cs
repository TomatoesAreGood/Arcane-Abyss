using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using static UnityEditor.Progress;
using System;
using System.Linq;


public class ItemLibrary : MonoBehaviour
{
    public object[] itemsArray;
    public Item[] Library;
    public static ItemLibrary instance;
    private Dictionary<Item, int> itemIDToReference;
    public GameObject coinPrefab;
    public SpellItem fireShot;
    public SpellItem iceShot;
    public SpellItem magicShot;
    public SpellItem windShot;
    public StaffItem basicStaff;
    public StaffItem forestStaff;
    public PotionItem healthPotion;
    public StaffItem darkStaff;
    public SpellBook fireShotSpellBook;
    public PotionItem smallHealthPot;
    public PotionItem smallManaPot;
    public PotionItem manaPotion;
    public SpellBook iceShotSpellBook;
    public SpellBook windShotSpellBook;
    public StaffItem iceStaff;
    public StaffItem demonicEyeStaff;
    public StaffItem holyStaff;
    public StaffItem tridentStaff;
    public StaffItem undeadStaff;
    public UsableItem orbofhoarding;
    public Key key;

    // Start is called before the first frame update
    private void Awake(){
        if(instance == null){
            instance = this;
            Library = new Item[] { basicStaff, forestStaff, healthPotion, darkStaff, fireShotSpellBook, 
            smallHealthPot, iceStaff, demonicEyeStaff, holyStaff,tridentStaff,manaPotion,smallManaPot,
            iceShotSpellBook, windShotSpellBook,orbofhoarding
            };
            itemIDToReference = new Dictionary<Item, int>{
                {fireShot,1}, {iceShot,2},{magicShot,3},{windShot,4},{basicStaff,100},
                {forestStaff,101},{healthPotion,10},{darkStaff,102},{fireShotSpellBook,20},{smallHealthPot,12} , 
                {smallManaPot, 13}, {manaPotion, 11}, {iceShotSpellBook, 21}, {windShotSpellBook, 22}, {iceStaff, 103},
                {demonicEyeStaff, 104}, {holyStaff, 105}, {tridentStaff, 106}, {undeadStaff, 107}, {key, 200},{orbofhoarding,201}
            };

            InitalizeItemToArray();
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    public Item GetReferenceFromID(int id){
        if(itemIDToReference.ContainsValue(id)){
            return itemIDToReference.FirstOrDefault(x=>x.Value == id).Key;
        }
        return null;
    }

    public int GetIDFromReference(Item item) {
        foreach (KeyValuePair<Item, int> kvp in itemIDToReference) {
            if (kvp.Key.GetType().Name == item.GetType().Name) {
                return kvp.Value;
            }
        }
        return 0;
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
            return fireShot;
        }else if(item.GetType() == typeof(IceSpellItem)){
            return iceShot;
        }else if (item.GetType() == typeof(DarkStaff)){
            return darkStaff;
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
        }else if (item.GetType() == typeof(SmallManaPotion)){
            return smallManaPot;
        }else if (item.GetType() == typeof(ManaPotion)){
            return manaPotion;
        }else if (item.GetType() == typeof(IceShotSpellBook)){
            return iceShotSpellBook;
        }else if (item.GetType() == typeof(WindShotSpellBook)){
            return windShotSpellBook;
        }else if (item.GetType() == typeof(IceStaff)){
            return iceStaff;
        }else if (item.GetType() == typeof(DemonicEyeStaff)){
            return demonicEyeStaff;
        }else if (item.GetType() == typeof(HolyStaff)){
            return holyStaff;
        }else if (item.GetType() == typeof(TridentStaff)){
            return tridentStaff;
        }else if (item.GetType() == typeof(UndeadStaff)){
            return undeadStaff;
        }else if (item.GetType() == typeof(Key)){
            return key;
        }
        throw new ArgumentException("Could not find item reference");
    }

    
}
