using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInventory _inventory;
    public GameObject itembutton;
    void Start()
    {
        
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            for (int i = 0; i < _inventory.slots.Length; i++)
            {
                if (_inventory.IsFull[i] == false)
                {
                    _inventory.inventoryData[i] = itembutton;

                    Debug.Log("picked up item");
                    _inventory.IsFull[i] = true;
                    RectTransform resize = itembutton.GetComponent<RectTransform>();
                    resize.sizeDelta = new Vector2(8, 8);
                    Instantiate(itembutton, _inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
