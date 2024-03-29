using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Util;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public enum Renderers { 
    inventory,
    spells,
    potion,
    equippedSpells
}

public class InventoryRenderer : MonoBehaviour
{
    public Renderers RendererType;
    public GameObject Slot;
    public Vector2 BottomLeft;
    public int Width;
    public int Height;
    private Item[] _inventoryData;
    
    //gets coords of a 2d array from a screenpoint
    public Vector2 GetMatrixCoords(Vector2 bottomLeft, Vector2 screenPoint){
        bottomLeft = new Vector2(bottomLeft.x -50 ,bottomLeft.y - 50);

        if(screenPoint.x < bottomLeft.x || screenPoint.x > bottomLeft.x + Width*100 ){
            return Vector2.negativeInfinity;
        }
        if(screenPoint.y < bottomLeft.y || screenPoint.y > bottomLeft.y + Height*100){
            return Vector2.negativeInfinity;
        }

        Vector2 coord = new Vector2(screenPoint.x - bottomLeft.x -50 , screenPoint.y - bottomLeft.y + 50);

        return new Vector2( Mathf.Round(coord.y/100f) -1 ,Mathf.Round(coord.x/100f));
    }

    private void Start(){
        //set inventory data which it writes to/updates
        BottomLeft = Camera.main.WorldToScreenPoint(transform.position);
        if(RendererType == Renderers.inventory){
            _inventoryData = PlayerController.Instance.inventory.Items;
        }else if(RendererType == Renderers.spells){
            _inventoryData = PlayerController.Instance.inventory.Spells;
        }else if(RendererType == Renderers.potion){
            _inventoryData = PlayerController.Instance.inventory.Potions;
        }else if(RendererType == Renderers.equippedSpells){
            _inventoryData = PlayerController.Instance.inventory.EquippedSpells;
        }
        DrawMatrix(Height,Width);
        //bring to the front
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100);
    }

    private void Update(){

       // update equipped spell icons
       if(PlayerController.Instance.inventoryUI.IsOpen && RendererType == Renderers.equippedSpells){
            for(int i = 0; i < 4; i++){
                GetSlot(i).item = PlayerController.Instance.inventory.EquippedSpells[i];
            }
            RedrawMatrix();
       }
    }

    //draws a matrix from bottom left, given hieght and width
    public void DrawMatrix(int height, int width){
        for(int r = 0; r < height; r++){
            for(int c = 0; c < width; c++){
                GameObject slot = Instantiate(Slot, transform);
                slot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(BottomLeft.x + 100*c, BottomLeft.y+ 100*r));
                int i = r*width + c;

                if(_inventoryData[i] == null){
                    continue;
                }

                if(_inventoryData[i].GetType() == typeof(BasicStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.basicStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if(_inventoryData[i].GetType() == typeof(ForestStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.forestStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if(_inventoryData[i].GetType() == typeof(FireSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.fireShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if(_inventoryData[i].GetType() == typeof(IceSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.iceShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(DarkStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.darkStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(MagicSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.magicShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(HealthPotion)){
                    GameObject obj = Instantiate(ItemLibrary.instance.healthPotion.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(FireShotSpellBook)){
                    GameObject obj = Instantiate(ItemLibrary.instance.fireShotSpellBook.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(WindSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.windShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(SmallHealthPotion)){
                    GameObject obj = Instantiate(ItemLibrary.instance.smallHealthPot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(SmallManaPotion)){
                    GameObject obj = Instantiate(ItemLibrary.instance.smallManaPot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(ManaPotion)){
                    GameObject obj = Instantiate(ItemLibrary.instance.manaPotion.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(IceShotSpellBook)){
                    GameObject obj = Instantiate(ItemLibrary.instance.iceShotSpellBook.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(WindShotSpellBook)){
                    GameObject obj = Instantiate(ItemLibrary.instance.windShotSpellBook.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(IceStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.iceStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(DemonicEyeStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.demonicEyeStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(HolyStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.holyStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(UndeadStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.undeadStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(TridentStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.tridentStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(Key)){
                    GameObject obj = Instantiate(ItemLibrary.instance.key.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(OrbOfHoarding)){
                    GameObject obj = Instantiate(ItemLibrary.instance.orbofhoarding.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }


            }
        }
    }

    //used if the data is updated & UI needs to be redrawn
    public void RedrawMatrix(){
          for(int r = 0; r < Height; r++){
            for(int c = 0; c < Width; c++){
                int i = r*Width + c;

                GameObject slot = GetTransform(i).gameObject;

                if(!GetSlot(i).IsEmpty()){
                    Destroy(GetTransform(i).GetChild(0).gameObject);
                }
                
                if(_inventoryData[i] == null){
                    continue;
                }

                if(_inventoryData[i].GetType() == typeof(BasicStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.basicStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if(_inventoryData[i].GetType() == typeof(ForestStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.forestStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if(_inventoryData[i].GetType() == typeof(FireSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.fireShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if(_inventoryData[i].GetType() == typeof(IceSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.iceShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(DarkStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.darkStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(MagicSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.magicShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(HealthPotion)){
                    GameObject obj = Instantiate(ItemLibrary.instance.healthPotion.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(FireShotSpellBook)){
                    GameObject obj = Instantiate(ItemLibrary.instance.fireShotSpellBook.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(WindSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.windShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(SmallHealthPotion)){
                    GameObject obj = Instantiate(ItemLibrary.instance.smallHealthPot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(SmallManaPotion)){
                    GameObject obj = Instantiate(ItemLibrary.instance.smallManaPot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(ManaPotion)){
                    GameObject obj = Instantiate(ItemLibrary.instance.manaPotion.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(IceShotSpellBook)){
                    GameObject obj = Instantiate(ItemLibrary.instance.iceShotSpellBook.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(WindShotSpellBook)){
                    GameObject obj = Instantiate(ItemLibrary.instance.windShotSpellBook.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(IceStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.iceStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(DemonicEyeStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.demonicEyeStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(HolyStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.holyStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(UndeadStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.undeadStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(TridentStaff)){
                    GameObject obj = Instantiate(ItemLibrary.instance.tridentStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(Key)){
                    GameObject obj = Instantiate(ItemLibrary.instance.key.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (_inventoryData[i].GetType() == typeof(OrbOfHoarding)){
                    GameObject obj = Instantiate(ItemLibrary.instance.orbofhoarding.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }
            }
        }

        // for(int i = 0; i < transform.childCount; i++){
        //     Destroy(transform.GetChild(i).gameObject);
        // }
        // DrawMatrix(height,width);
        // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100);
    }

    //writes data into the inventory 
    public void UpdateData(){
        for(int i = 0; i < transform.childCount; i++){
            GetSlot(i).UpdateData();
            if(GetSlot(i).IsEmpty()){
                _inventoryData[i] = null;
            }else{
                _inventoryData[i] = GetSlot(i).item;
            }
        }
    }

    public Slot GetSlot(int index){
        return GetTransform(index).GetComponent<Slot>();
    }

    public Transform GetTransform(int index){
        return transform.GetChild(index);
    }

    //for equipped spells
    public void SelectSlot(int index){
        for(int i = 0; i < transform.childCount; i++){
           GetSlot(i).UnDim();
        }
        GetSlot(index).Dim();
    }

    //used when player picks up an item
    public void InstantiateItem(Item item, int index){
        if(GetSlot(index).IsEmpty()){
            GameObject obj = Instantiate(item.gameObject, GetTransform(index));
            obj.transform.position = GetTransform(index).position;
            obj.SetActive(true);
        }
    }

    //bubble sorts items by value, decreasing
    public void BubbleSortValue(){
        UpdateData();

        for (int j = 0; j < _inventoryData.Length; j++){
            for(int i = 0; i < _inventoryData.Length-1; i++){
                int value1 = 0;
                int value2 = 0;

                //if item == null, assign it a value of 0
                if(_inventoryData[i+1] != null){
                    value2 = _inventoryData[i+1].Value;
                }
                if(_inventoryData[i] != null){
                    value1 = _inventoryData[i].Value;
                }

                if(value1 < value2){
                    var temp = _inventoryData[i];
                    _inventoryData[i] = _inventoryData[i+1];
                    _inventoryData[i+1] = temp;
                }
            }
        }
        RedrawMatrix();
        UpdateData();
    }

    private bool ContainsKey( Dictionary<Item, int> itemCountDict, Item key){
        foreach(Item item in itemCountDict.Keys){
            if(key.GetType() == item.GetType()){
                return true;
            }   
        }
        return false;
    }


    //pigeonhole sorts, from most to least occurances 
    public void PigeonHoleSortOcurrances(){
        UpdateData();

        Dictionary<Item, int> itemCountDict = new Dictionary<Item, int>();
        
        for(int i = 0; i < _inventoryData.Length; i++){
            if(_inventoryData[i] == null){
                continue;
            }
            if(ContainsKey(itemCountDict,_inventoryData[i])){
                itemCountDict[_inventoryData[i]] += 1;
            }else{
                itemCountDict[_inventoryData[i]] = 1;
            }
            _inventoryData[i] = null;
        }

        int index = 0;

        while(itemCountDict.Count > 0){
            int max = -1;
            Item key = null;
            foreach(KeyValuePair<Item, int> kvp in itemCountDict){
                if(kvp.Value > max){
                    max = kvp.Value;
                    key = kvp.Key;
                }
            }
            for(int i = 0; i < max; i++){
                _inventoryData[index] = key;
                index++;
            }
            itemCountDict.Remove(key);
        }
        RedrawMatrix();
        UpdateData();
    }

    public void MergeSortSortAlpha(){
        UpdateData();
        MergeSortSortAlpha(_inventoryData);
        RedrawMatrix();
        UpdateData();
    }

    public void MergeSortSortAlpha(Item[] arr){
        if(arr.Length <= 1){
            return;
        }
       
        int mid = arr.Length / 2;

        Item[] leftArr = new Item[mid];
        Item[] rightArr = new Item[arr.Length - mid];

        for(int a = 0; a < leftArr.Length; a++){
            leftArr[a] = arr[a];
        }

        for(int b = 0; b < rightArr.Length; b++){
            rightArr[b] = arr[mid + b];
        }
        MergeSortSortAlpha(leftArr);
        MergeSortSortAlpha(rightArr);

        int i = 0;
        int j = 0;
        int k = 0;

        while(i < leftArr.Length && j < rightArr.Length){
            string leftString = "zzzzzzzzzzzzzzzzz";
            string rightString = "zzzzzzzzzzzzzzzzz";


            if(leftArr[i] != null){
                leftString = leftArr[i].Title;
            }
            if(rightArr[j] != null){
                rightString = rightArr[j].Title;
            }

            if(String.Compare(leftString, rightString) < 0){
                arr[k] = leftArr[i];
                i++;
            }else{
                arr[k] = rightArr[j];
                j++;
            }
            k++;
        }

        while(i < leftArr.Length){
            arr[k] = leftArr[i];
            i++;
            k++;
        }

        while(j < rightArr.Length){
            arr[k] = rightArr[j];
            j++;
            k++;
        }
        
    }
 


}
