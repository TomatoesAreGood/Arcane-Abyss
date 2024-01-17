using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;
using System;

public enum Renderers { 
    inventory,
    spells,
    potion,
    equippedSpells
}

public class Item : MonoBehaviour
{ 
    public int value;
    public string title;
    public string desc;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Image image;
    private RectTransform rectTransform;
    public bool IsMouseOnItem => RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main);
    public new InventoryRenderer renderer;
    public Item[] inventory;

    protected virtual void Awake(){
        image = gameObject.GetComponent<Image>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        parentAfterDrag = transform.parent;
        renderer = PlayerController.instance.inventoryUI.inventoryRenderer;
        inventory = PlayerController.instance.inventory.items;
    }

    protected virtual void Update(){
        if(!PlayerController.instance.inventoryUI.isOpen){
            return;
        }

        //on left click
        if(IsMouseOnItem && Input.GetMouseButtonDown(1)){
            MousePointer.instance.SetInteractingItem(this);
        }

        //on click
        if(IsMouseOnItem && Input.GetMouseButtonDown(0)){
            Vector2 coords = renderer.GetMatrixCoords(renderer.bottomLeft, Input.mousePosition);
            int index = (int)coords.x*renderer.width + (int)coords.y;
            renderer.GetSlot(index).item = null;

            parentAfterDrag = transform.parent; 
            image.raycastTarget = false;
        }
        //on drag
        if(Input.GetMouseButton(0) && IsMouseOnItem){
            if(MousePointer.instance.selectedItem == null){
                MousePointer.instance.SelectItem(this);
            }
        }
        //on drop
        if(MousePointer.instance.IsSelected(this) && Input.GetMouseButtonUp(0)){
            Vector2 coords = renderer.GetMatrixCoords(renderer.bottomLeft, Input.mousePosition);

            if (!coords.Equals(Vector2.negativeInfinity)){
                int index = (int)coords.x*renderer.width + (int)coords.y;

                if(renderer.GetSlot(index).IsEmpty()){
                    renderer.GetSlot(index).item = MousePointer.instance.selectedItem;
                    parentAfterDrag = renderer.GetTransform(index);
                }
            }
            transform.SetParent(parentAfterDrag);
            transform.position = parentAfterDrag.position;
            image.raycastTarget = false;
            MousePointer.instance.DeSelectItem();
        }
        
    }

    public void SnapBack(){
        transform.SetParent(parentAfterDrag);
        transform.position = parentAfterDrag.position;
        image.raycastTarget = false;
        MousePointer.instance.DeSelectItem();
    }


    public void Drop(){

    }
    public void Sell(){

    }

    public virtual void Use(){
        throw new ArgumentException("item is not usable");
    }
  
    public virtual void Equip(){
        throw new ArgumentException("item is not a staff");
    }

    public virtual void EquipSpellSlot1(){
        throw new ArgumentException("item is not a spell");
    }
    public virtual void EquipSpellSlot2(){
        throw new ArgumentException("item is not a spell");

    } 
    public virtual void EquipSpellSlot3(){
        throw new ArgumentException("item is not a spell");

    } 
    public virtual void EquipSpellSlot4(){
        throw new ArgumentException("item is not a spell");
    }

   

}

