using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory _inventory;
    private Item[] _inventoryData;
    [SerializeField] Item itemReference;

    
    private void Start()
    {
        _inventory = PlayerController.instance.inventory;

        if(itemReference.renderer == PlayerController.instance.inventoryUI.inventoryRenderer){
            _inventoryData = _inventory.items;
        }else if(itemReference.renderer == PlayerController.instance.inventoryUI.potionBagRenderer){
            _inventoryData = _inventory.potions;
        }
               
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            
            // for (int i = 0; i < _inventoryData.Length; i++) {
            //     if (_inventoryData[i] == null){

            //         _inventoryData[i] = itemReference;

            //         Debug.Log("picked up item");

            //         Destroy(gameObject);
            //         break;
            //     }
            // }
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
