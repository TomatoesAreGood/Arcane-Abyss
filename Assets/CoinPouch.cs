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
        _coinPatchValue = Random.Range(5, 10);
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.money += _coinPatchValue;
            Destroy(gameObject);
        }
    }


}