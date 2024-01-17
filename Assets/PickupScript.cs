using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Item itemReference;

    
    private void Start()
    {
          
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerIsTrigger")){
            if(PlayerController.instance.TryPickUp(itemReference)){
                Debug.Log("picked up: " + itemReference.ToString());
                Destroy(gameObject);
            }else{
                Debug.Log("inventory full");
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
    }
}
