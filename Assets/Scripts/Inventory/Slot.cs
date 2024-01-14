using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;


public class Slot : MonoBehaviour
{
    public Item item;
    private Image image;
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
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(IsMouseHovering){
            SetAlpha(0.5f);
        }else{
            SetAlpha(1f);
        }
    }
}
