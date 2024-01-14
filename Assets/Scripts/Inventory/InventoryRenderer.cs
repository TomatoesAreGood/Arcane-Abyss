using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class InventoryRenderer : MonoBehaviour
{
    public Renderers rendererType;
    public GameObject Slot;
    public Vector2 bottomLeft;
    public int width;
    public int height;
        
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
        DrawMatrix(height,width);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100);
    }

    public void DrawMatrix(int height, int width){
        Inventory inventory = PlayerController.instance.inventory;

        if(rendererType == Renderers.inventory){
            for(int r = 0; r < height; r++){
                for(int c = 0; c < width; c++){
                    GameObject slot = Instantiate(Slot, transform);
                    slot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(bottomLeft.x + 100*c, bottomLeft.y+ 100*r));

                    if(inventory.items[r*PlayerController.instance.inventoryWidth + c] == null){
                        continue;
                    }

                    if(inventory.items[r*PlayerController.instance.inventoryWidth + c].GetType() == typeof(StaffItem)){
                        GameObject obj = Instantiate(ItemLibrary.instance.basicStaff, slot.transform);
                        obj.transform.position = slot.transform.position;
                    }
                }
            }
        }else if (rendererType == Renderers.spells){
            for(int r = 0; r < height; r++){
                for(int c = 0; c < width; c++){
                    GameObject slot = Instantiate(Slot, transform);
                    slot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(bottomLeft.x + 100*c, bottomLeft.y+ 100*r));

                    if(inventory.spells[r*PlayerController.instance.inventoryWidth + c] == null){
                        continue;
                    }
                }
            }
        }else if(rendererType == Renderers.potion){
             for(int r = 0; r < height; r++){
                for(int c = 0; c < width; c++){
                    GameObject slot = Instantiate(Slot, transform);
                    slot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(bottomLeft.x + 100*c, bottomLeft.y+ 100*r));

                    if(inventory.potions[r*PlayerController.instance.inventoryWidth + c] == null){
                        continue;
                    }
                }
            }
        }
      
    }

    public Transform GetTransform(int index){
        return transform.GetChild(index);

    }
}
