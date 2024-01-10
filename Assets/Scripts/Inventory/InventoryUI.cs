using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject Slot;
    public static InventoryUI instance;
    public bool isOpen;
    private GameObject inventory;
    private GameObject equippedStaff;
    private GameObject potionBag;


    //items
    public GameObject StaffItem;
     

    private void Start()
    {
        if (instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
        inventory = transform.GetChild(0).gameObject;
        equippedStaff = transform.GetChild(1).gameObject;
        potionBag = transform.GetChild(2).gameObject;

        int row = PlayerController.instance.inventorySize.Item1;
        int col = PlayerController.instance.inventorySize.Item2;

        Vector2 origin = inventory.transform.position;

        for (int r = 0; r < row; r++){
            for(int c = 0; c < col; c++){
                GameObject slot = Instantiate(Slot, inventory.transform);
                slot.transform.position = new Vector2(origin.x + c*0.9f, origin.y+ -r*0.9f);
                if (r%2==0){
                    Instantiate(StaffItem, slot.transform);
                }
            }
        }

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
    void Update()
    {       

        if (isOpen){
            gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
        }
    }
}
