using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 
    public int length;
    public int width;
    public int value;
    public string title;
    public string desc;
    [HideInInspector] public Transform parentAfterDrag;
    private Image image;

    // Start is called before the first frame update
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void Drop(){

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent; 
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;


    }


}


