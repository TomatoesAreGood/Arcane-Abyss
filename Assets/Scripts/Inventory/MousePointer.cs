using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public InteractPanel interactPanel;
    public static MousePointer instance;
    public Item selectedItem;
    public bool isInteracting;
    public Item interactingItem;

    private void Start(){
        instance = this;
        isInteracting = false;
        // gameObject.GetComponent<Collider>().enabled = false;
    }

    private void Update(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 10f);

        if(Input.GetKeyUp(KeyCode.E) && selectedItem != null){
            DeSelectItem();
        }

        if(!interactPanel.IsMouseOnItem && Input.GetMouseButtonDown(0)){
            interactPanel.ResetPos();
            interactPanel.transform.SetParent(transform);
            interactingItem = null;
            isInteracting = false;
        }

        if (isInteracting){
            interactPanel.OpenPanel(interactingItem);
        }else{
            interactPanel.ClosePanel();
        }

        // if(selectedItem != null){
        //     selectedItem.transform.SetParent(transform);
        // }

        // if(Input.GetMouseButton(0)){

        // }

    }

    public void SetInteractingItem(Item item){
        isInteracting = true;
        interactingItem = item;
    }

    public void SelectItem(Item item){
        selectedItem = item;
        selectedItem.transform.SetParent(transform);

    }
    public void DeSelectItem(){
        
        selectedItem = null;
    }

    public bool IsSelected(Item item) {
        return selectedItem == item;
    }

    public void Use(){
        interactingItem.Use();
    }
    public void Sell(){
        interactingItem.Sell();
    }
    public void Drop(){
        interactingItem.Drop();
    }
    public void Equip(){
        interactingItem.Equip();
    }
    public void EquipSpellSlot1(){
        interactingItem.EquipSpellSlot1();
    }
    public void EquipSpellSlot2(){
        interactingItem.EquipSpellSlot2();
    } 
    public void EquipSpellSlot3(){
        interactingItem.EquipSpellSlot3();

    } 
    public void EquipSpellSlot4(){
        interactingItem.EquipSpellSlot4();

    }

}
