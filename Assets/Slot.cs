using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Item item = dropped.GetComponent<Item>();
        item.parentAfterDrag = transform;

    }

    // Start is called before the first frame update
    void Start()
    {
    
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
