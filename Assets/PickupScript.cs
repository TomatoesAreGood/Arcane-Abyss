using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
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
                    

                    Debug.Log("picked up item");
                    inventory.IsFull[i] = true;
                    RectTransform resize = itembutton.GetComponent<RectTransform>();
                    resize.sizeDelta = new Vector2(250, 250);
                    Instantiate(itembutton, inventory.slots[i].transform, false);
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
