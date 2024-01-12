using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

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


    // Start is called before the first frame update
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        parentAfterDrag = transform.parent; 
        
        renderer = InventoryRenderer.instance;
        inventory = PlayerController.instance.inventory.items;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //on click
        if(IsMouseOnItem && Input.GetMouseButtonDown(0)){
            Vector2 coords = renderer.GetMatrixCoords(renderer.bottomLeft, Input.mousePosition);
            int index = (int)coords.x*PlayerController.instance.inventoryWidth + (int)coords.y;

            PlayerController.instance.inventory.items[index] = null;
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
            int index = (int)coords.x*PlayerController.instance.inventoryWidth + (int)coords.y;
            Debug.Log(coords.x + " " + coords.y);
            Debug.Log(index);
            if(!coords.Equals(Vector2.negativeInfinity)){
                if (inventory[index] == null){
                    inventory[index] = this;
                    parentAfterDrag = renderer.GetTransform(index);
                }
            }
            transform.SetParent(parentAfterDrag);
            transform.position = parentAfterDrag.position;
            image.raycastTarget = false;
            MousePointer.instance.DeSelectItem();
        }
      
    }
    protected void Drop(){

    }


   

}


