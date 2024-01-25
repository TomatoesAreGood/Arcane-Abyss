using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerIsTrigger"))
        {
            PlayerController.instance.coins += 1;
            Destroy(gameObject);
            SoundManager.instance.PlayCoinPickUpSFX();
        }
    }
}