using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public static MousePointer instance;
    public Item selectedItem;
    private void Start(){
        instance = this;
        // gameObject.GetComponent<Collider>().enabled = false;
    }

    private void Update(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 10f);

        if(Input.GetKeyUp(KeyCode.E) && selectedItem != null){
            DeSelectItem();
        }

        // if(selectedItem != null){
        //     selectedItem.transform.SetParent(transform);
        // }

        // if(Input.GetMouseButton(0)){

        // }



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
  
}
