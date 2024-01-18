using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;


public class Slot : MonoBehaviour
{
    public Item item;
    public Image image;
    private bool IsMouseHovering => RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main);
    private RectTransform rectTransform;


    public bool IsEmpty(){
        //return item == null;
        return transform.childCount == 0;
    }
  
    private void SetAlpha(float alpha){
        var tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        item = null;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateData();
        if(PlayerController.instance.inventoryUI.isOpen){
            if(IsMouseHovering){
                Dim();
            }else{
                UnDim();
            } 
        }       
    }

    public void UpdateData(){
        if(!IsEmpty()){
            item = transform.GetChild(0).GetComponent<Item>();
        }else{
            item = null;
        }
    }

    public void Dim(){
        SetAlpha(0.5f);
    }
    public void UnDim(){
        SetAlpha(1f);
    }
}
