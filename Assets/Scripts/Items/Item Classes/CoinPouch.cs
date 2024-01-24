using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPouch : MonoBehaviour
{
    // Start is called before the first frame update
    private int _coinPatchValue;
     
   
    void Start()
    {
        _coinPatchValue = Random.Range(10, 20);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerIsTrigger"))
        {
            PlayerController.instance.coins += _coinPatchValue;
            Destroy(gameObject);
            SoundManager.instance.PlayCoinPickUpSFX();
        }
    }


}
