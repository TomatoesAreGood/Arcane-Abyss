using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

 using System.Runtime.Serialization.Formatters.Binary;
 using System.IO;
 
[Serializable]
public class Item : MonoBehaviour
{ 
    public int Value;
    public string Title;
    public string Desc;
    public Renderers ItemType;
    [HideInInspector] public Transform ParentAfterDrag;
    [HideInInspector] public Image Image;
    private RectTransform _rectTransform;
    public bool IsMouseOnItem => RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, Input.mousePosition, Camera.main);
    public InventoryRenderer Renderer;
    public Item[] Inventory;
    public int ItemID {get; set;}

    protected virtual void Awake(){
        Image = gameObject.GetComponent<Image>();
        _rectTransform = gameObject.GetComponent<RectTransform>();
        ParentAfterDrag = transform.parent;
        Renderer = PlayerController.Instance.inventoryUI.InventoryRenderer;
        Inventory = PlayerController.Instance.inventory.Items;
    }    
    protected virtual void Start(){
        Value = 0;
        Desc = "bro forgor description";
        Title = GetType().Name;
        ItemID = 0;
    }

    protected virtual void Update(){
        if(!PlayerController.Instance.inventoryUI.IsOpen){
            return;
        }

        if(IsMouseOnItem){
            MousePointer.instance.hoveringItem = this;
        }

        //on left click
        if(IsMouseOnItem && Input.GetMouseButtonDown(1)){
            MousePointer.instance.SetInteractingItem(this);
        }

        //on click
        if(IsMouseOnItem && Input.GetMouseButtonDown(0)){
            Vector2 coords = Renderer.GetMatrixCoords(Renderer.BottomLeft, Input.mousePosition);
            int index = (int)coords.x*Renderer.Width + (int)coords.y;
            Renderer.GetSlot(index).item = null;

            ParentAfterDrag = transform.parent; 
            Image.raycastTarget = false;
            Renderer.UpdateData();
        }
        //on drag
        if(Input.GetMouseButton(0) && IsMouseOnItem){
            if(MousePointer.instance.selectedItem == null){
                MousePointer.instance.SelectItem(this);
            }
            MousePointer.instance.hoveringItem = null;
        }
        //on drop
        if(MousePointer.instance.IsSelected(this) && Input.GetMouseButtonUp(0)){
            Vector2 coords = Renderer.GetMatrixCoords(Renderer.BottomLeft, Input.mousePosition);

            if (!coords.Equals(Vector2.negativeInfinity)){
                int index = (int)coords.x*Renderer.Width + (int)coords.y;

                if(Renderer.GetSlot(index).IsEmpty()){
                    Renderer.GetSlot(index).item = MousePointer.instance.selectedItem;
                    ParentAfterDrag = Renderer.GetTransform(index);
                }
            }
            transform.SetParent(ParentAfterDrag);
            transform.position = ParentAfterDrag.position;
            Image.raycastTarget = false;
            MousePointer.instance.DeSelectItem();
            Renderer.UpdateData();
        }

    }

    public void SnapBack(){
        transform.SetParent(ParentAfterDrag);
        transform.position = ParentAfterDrag.position;
        Image.raycastTarget = false;
        MousePointer.instance.DeSelectItem();
    }


    public virtual void Drop(){
        GameObject obj = Instantiate(PickUpController.instance.defaultDropItem);
        obj.transform.position = PlayerController.CharacterPos;
        obj.GetComponent<PickupScript>().itemReference = ItemLibrary.instance.GetItemReference(this);
        Destroy(gameObject);
    }

    public virtual void Drop(Vector2 location)
    {
        GameObject obj = Instantiate(PickUpController.instance.defaultDropItem);
        obj.transform.position = location;
        obj.GetComponent<PickupScript>().itemReference = ItemLibrary.instance.GetItemReference(this);
/*        Destroy(gameObject);
*/    }

    public void Sell(){
        PlayerController.Instance.Coins += Value;
        Destroy(gameObject);
        SoundManager.instance.PlaySellItemSFX();
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

    //for PigeonholeSort
    public override int GetHashCode()             
    {  
        return ItemID; 
    }

   public override bool Equals(object obj) 
    { 
        return Equals(obj as Item); 
    }

    public bool Equals(Item obj)
    { 
        return GetType() == obj.GetType(); 
    }


}


