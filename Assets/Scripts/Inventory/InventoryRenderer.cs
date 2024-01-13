using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class InventoryRenderer : MonoBehaviour
{
    public GameObject Slot;
    public bool isOpen;
    public Vector2 bottomLeft;
        
    public Vector2 GetMatrixCoords(Vector2 bottomLeft, Vector2 screenPoint){
        bottomLeft = new Vector2(bottomLeft.x - 50, bottomLeft.y-50);

        if(screenPoint.x < bottomLeft.x || screenPoint.x > bottomLeft.x + PlayerController.instance.inventoryWidth*100){
            return Vector2.negativeInfinity;
        }
        if(screenPoint.y < bottomLeft.y || screenPoint.y > bottomLeft.y + PlayerController.instance.inventoryHeight*100){
            return Vector2.negativeInfinity;
        }

        Vector2 coord = new Vector2(screenPoint.x - bottomLeft.x - 50, screenPoint.y - bottomLeft.x - 50);

        return new Vector2( Mathf.Round(coord.y/100f) -1 ,Mathf.Round(coord.x/100f) );
    }

    private void Start()
    {
        bottomLeft = Camera.main.WorldToScreenPoint(transform.position);


        Vector2 origin = Camera.main.WorldToScreenPoint(transform.position);

        DrawMatrix(PlayerController.instance.inventoryHeight,PlayerController.instance.inventoryWidth);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100);

        // RectTransform rt = inventory.GetComponent<RectTransform>();
        // rt.sizeDelta = new Vector2(gridLen*100, gridRow*100);
        // for (int i= 0; i < gridRow * gridLen; i++){
        //     Instantiate(Slot, inventory.transform);
        // }

        isOpen = false;
    }

    public void DrawMatrix(int height, int width){
        Inventory inventory = PlayerController.instance.inventory;
        for(int i = 0; i < inventory.size; i++){
            Console.WriteLine(inventory.items[i]);
        }

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
    }

    public void Enable(){
        Time.timeScale = 0f;
        isOpen = true;
        gameObject.SetActive(true);
    }
    public void Disable(){
        Time.timeScale = 1f;
        isOpen = false;
        gameObject.SetActive(false);

    }


    // Update is called once per frame
    private void Update() {    
        if (isOpen){
            gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
        }

    }

    public Transform GetTransform(int index){
        return transform.GetChild(index);

    }
}
