using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{ 
    public int length;
    public int width;
    public int value;
    public string title;
    public string desc;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Image image;
    private RectTransform rectTransform;
    public bool IsMouseOnItem => RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main);


    // Start is called before the first frame update
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        parentAfterDrag = transform.parent; 
    }

    // Update is called once per frame
    protected void Update()
    {
        //on click
        if(IsMouseOnItem && Input.GetMouseButtonDown(0)){
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
            Vector2 coords = InventoryUI.instance.GetMatrixCoords(Input.mousePosition);
            if(!coords.Equals(Vector2.negativeInfinity)){
                if (PlayerController.instance.inventory.itemGrid[(int)coords.y, (int)coords.x] == null){
                    parentAfterDrag = InventoryUI.instance.inventory.transform.GetChild((int)coords.y*PlayerController.instance.inventorySize.Item2 + (int)coords.x);
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


