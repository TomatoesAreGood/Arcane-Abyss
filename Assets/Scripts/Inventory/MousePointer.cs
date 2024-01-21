using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public InteractPanel interactPanel;
    public InfoPanel infoPanel;
    public static MousePointer instance;
    public Item selectedItem;
    public bool isInteracting;
    public Item interactingItem;
    public Item hoveringItem;

    private void Start(){
        instance = this;
        isInteracting = false;
        // gameObject.GetComponent<Collider>().enabled = false;
    }

    private void Update(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 10f);

        if(hoveringItem == null || interactingItem != null){
            infoPanel.ClosePanel();
        }

        if(interactingItem == null && hoveringItem != null){
            infoPanel.OpenPanel(hoveringItem);
        }

        if(Input.GetKeyUp(KeyCode.E) && selectedItem != null){
            DeSelectItem();
        }

        if(!interactPanel.IsMouseHovering && Input.GetMouseButton(0)){
            interactPanel.ResetPos();
            interactPanel.transform.SetParent(transform);
            isInteracting = false;
            interactingItem = null;
        }
      
        if(!isInteracting){
            interactPanel.ClosePanel();
        }

        hoveringItem = null;
    }

    public void SetInteractingItem(Item item){
        isInteracting = true;
        interactingItem = item;
        interactPanel.OpenPanel(interactingItem);
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
        interactPanel.ClosePanel();
    }
    public void Sell(){
        interactingItem.Sell();
        interactPanel.ClosePanel();
    }
    public void Drop(){
        interactingItem.Drop();
        interactPanel.ClosePanel();
    }
    public void Equip(){
        interactingItem.Equip();
        interactPanel.ClosePanel();
    }
    public void EquipSpellSlot1(){
        interactingItem.EquipSpellSlot1();
        interactPanel.ClosePanel();
    }
    public void EquipSpellSlot2(){
        interactingItem.EquipSpellSlot2();
        interactPanel.ClosePanel();
    } 
    public void EquipSpellSlot3(){
        interactingItem.EquipSpellSlot3();
        interactPanel.ClosePanel();
    } 
    public void EquipSpellSlot4(){
        interactingItem.EquipSpellSlot4();
        interactPanel.ClosePanel();
    }

}
