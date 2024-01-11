using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject Slot;
    public static InventoryUI instance;
    public bool isOpen;
    public GameObject inventory;
    private GameObject equippedStaff;
    private GameObject potionBag;


    //items
    public GameObject StaffItem;
     
    public Vector2 GetMatrixCoords(Vector2 screenPoint){
        Vector2 origin = Camera.main.WorldToScreenPoint(inventory.transform.position);
        origin = new Vector2(origin.x - 50, origin.y-50);
        // screenPoint = Camera.main.ScreenToWorldPoint(screenPoint);

        if(screenPoint.x < origin.x || screenPoint.x > origin.x + PlayerController.instance.inventorySize.Item2*100){
            return Vector2.negativeInfinity;
        }
        if(screenPoint.y < origin.y || screenPoint.y > origin.y + PlayerController.instance.inventorySize.Item1*100){
            return Vector2.negativeInfinity;
        }

        Vector2 coord = new Vector2(screenPoint.x - origin.x - 50, screenPoint.y - origin.x - 50);

        return new Vector2(Mathf.Round(coord.x/100f),Mathf.Round(coord.y/100f) + PlayerController.instance.inventorySize.Item1);
    }

    private void Start()
    {

        if (instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
        inventory = transform.GetChild(1).gameObject;
        equippedStaff = transform.GetChild(3).gameObject;
        potionBag = transform.GetChild(2).gameObject;

        int row = PlayerController.instance.inventorySize.Item1;
        int col = PlayerController.instance.inventorySize.Item2;

        Vector2 origin = Camera.main.WorldToScreenPoint(inventory.transform.position);

        for (int r = 0; r < row; r++){
            for(int c = 0; c < col; c++){
                GameObject slot = Instantiate(Slot, inventory.transform);
                slot.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(origin.x + 100*c, origin.y+ 100*r));

                if(PlayerController.instance.inventory.itemGrid[row, col] == StaffItem){
                    GameObject obj = Instantiate(StaffItem, slot.transform);
                    obj.transform.position = slot.transform.position;
                }
               
            }
        }
        inventory.transform.position = new Vector3(inventory.transform.position.x, inventory.transform.position.y, inventory.transform.position.z + 100);

        // RectTransform rt = inventory.GetComponent<RectTransform>();
        // rt.sizeDelta = new Vector2(gridLen*100, gridRow*100);
        // for (int i= 0; i < gridRow * gridLen; i++){
        //     Instantiate(Slot, inventory.transform);
        // }

        isOpen = false;
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
        Debug.Log(GetMatrixCoords(Input.mousePosition));
   
        if (isOpen){
            gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
        }

    }

    private void UpdateInventory(){
       
    }
}
