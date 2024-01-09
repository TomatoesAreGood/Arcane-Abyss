using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInventory inventory;
    public GameObject itembutton;
    void Start()
    {
        
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.IsFull[i] == false)
                {
                    //ADD ITEM TO INVENTORY
                    Debug.Log("picked up item");
                    inventory.IsFull[i] = true;
                    //Instantiate(itembutton, inventory.slots[i].transform);
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
