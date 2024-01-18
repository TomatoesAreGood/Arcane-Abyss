using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Item itemReference;
    private SpriteRenderer sr;
    [SerializeField] GameObject inventoryFullText;
    private float pickUpRange = 2;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = itemReference.GetComponent<Image>().sprite;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("PlayerIsTrigger")){
    //         if(PlayerController.instance.TryPickUp(itemReference)){
    //             Debug.Log("picked up: " + itemReference.ToString());
    //             Destroy(gameObject);
    //         }else{
    //             StartCoroutine(FullInventory());
    //         }
    //     }
    // }

    private IEnumerator FullInventory(){
        inventoryFullText.SetActive(true);
        yield return new WaitForSeconds(1);
        inventoryFullText.SetActive(false);
    }

    private void Update(){
        if(DistFromPlayer() <= pickUpRange){
            PickUpController.instance.canPickUp = true;
            if(PickUpController.instance.isPickingUp){
                PickUpController.instance.isPickingUp = false;
                if(PickUpController.instance.TryPickUp(itemReference)){
                    Debug.Log("picked up: " + itemReference.ToString());
                    Destroy(gameObject);
                }else{
                    StartCoroutine(FullInventory());
                }
            }
        }
    }

    private float DistFromPlayer(){
        Vector2 difference = PlayerController.characterPos - transform.position;
        return Mathf.Sqrt(Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2));
    }
}
