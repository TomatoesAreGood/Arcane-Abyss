using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class PickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Item itemReference;
    private SpriteRenderer sr;
    [SerializeField] GameObject inventoryFullText;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = itemReference.GetComponent<Image>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerIsTrigger")){
            if(PlayerController.instance.TryPickUp(itemReference)){
                Debug.Log("picked up: " + itemReference.ToString());
                Destroy(gameObject);
            }else{
                StartCoroutine(FullInventory());
            }
        }
    }

    private IEnumerator FullInventory(){
        inventoryFullText.SetActive(true);
        yield return new WaitForSeconds(1);
        inventoryFullText.SetActive(false);
    }




    // Update is called once per frame
    void Update()
    {
    }
}
