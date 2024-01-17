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
    public Renderers rendererType;
    public GameObject Slot;
    public Vector2 bottomLeft;
    public int width;
    public int height;
    private Item[] inventoryData;
        
    public Vector2 GetMatrixCoords(Vector2 bottomLeft, Vector2 screenPoint){
        bottomLeft = new Vector2(bottomLeft.x -50 ,bottomLeft.y - 50);

        if(screenPoint.x < bottomLeft.x || screenPoint.x > bottomLeft.x + width*100 ){
            return Vector2.negativeInfinity;
        }
        if(screenPoint.y < bottomLeft.y || screenPoint.y > bottomLeft.y + height*100){
            return Vector2.negativeInfinity;
        }

        Vector2 coord = new Vector2(screenPoint.x - bottomLeft.x -50 , screenPoint.y - bottomLeft.y + 50);

        return new Vector2( Mathf.Round(coord.y/100f) -1 ,Mathf.Round(coord.x/100f));
    }

    private void Start(){
        bottomLeft = Camera.main.WorldToScreenPoint(transform.position);
        if(rendererType == Renderers.inventory){
            inventoryData = PlayerController.instance.inventory.items;
        }else if(rendererType == Renderers.spells){
            inventoryData = PlayerController.instance.inventory.spells;
        }else if(rendererType == Renderers.potion){
            inventoryData = PlayerController.instance.inventory.potions;
        }else if(rendererType == Renderers.equippedSpells){
            inventoryData = PlayerController.instance.inventory.equippedSpells;
        }
        DrawMatrix(height,width);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100);
    }

    private void Update(){
       if(PlayerController.instance.inventoryUI.isOpen && rendererType == Renderers.equippedSpells){
            for(int i = 0; i < 4; i++){
                GetSlot(i).item = PlayerController.instance.inventory.equippedSpells[i];
            }
            RedrawMatrix();
       }
    }

    public void DrawMatrix(int height, int width){
        for(int r = 0; r < height; r++){
            for(int c = 0; c < width; c++){
                GameObject slot = Instantiate(Slot, transform);
                slot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(bottomLeft.x + 100*c, bottomLeft.y+ 100*r));
                int i = r*width + c;

                if(inventoryData[i] == null){
                    continue;
                }

                if(inventoryData[i].GetType() == typeof(BasicStaffItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.basicStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if(inventoryData[i].GetType() == typeof(ForestStaffItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.forestStaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if(inventoryData[i].GetType() == typeof(FireSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.fireball.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if(inventoryData[i].GetType() == typeof(IceSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.iceShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (inventoryData[i].GetType() == typeof(DarkStaffItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.darkstaff.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (inventoryData[i].GetType() == typeof(MagicSpellItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.magicShot.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }else if (inventoryData[i].GetType() == typeof(HealthPotionItem)){
                    GameObject obj = Instantiate(ItemLibrary.instance.healthPotion.gameObject, slot.transform);
                    obj.transform.position = slot.transform.position;
                }


            }
        }
    }

    public void RedrawMatrix(){
        for(int i = 0; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
        DrawMatrix(height,width);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100);
    }

    public void UpdateData(){
        for(int i = 0; i < transform.childCount; i++){
            if(transform.GetChild(i).GetComponent<Slot>().IsEmpty()){
                inventoryData[i] = null;
            }else{
                inventoryData[i] = GetSlot(i).item;
            }
        }
    }

    public Slot GetSlot(int index){
        return GetTransform(index).GetComponent<Slot>();
    }

    public Transform GetTransform(int index){
        return transform.GetChild(index);
    }

    public void SelectSlot(int index){
        for(int i = 0; i < transform.childCount; i++){
           GetSlot(i).UnDim();
        }
        GetSlot(index).Dim();
    }

    public void InstantiateItem(Item item, int index){
        if(GetSlot(index).IsEmpty()){
            Instantiate(item.gameObject, GetTransform(index)).transform.position = GetTransform(index).position;
        }
    }
}
