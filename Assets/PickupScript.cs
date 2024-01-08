using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInventory inventory;
    void Start()
    {
        
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Player"))){
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.IsFull[i] == false)
                {
                    //ADD ITEM TO INVENTORY
                    
                }
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
