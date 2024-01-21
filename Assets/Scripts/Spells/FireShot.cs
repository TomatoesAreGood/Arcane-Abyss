using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MagicShot
{
   protected override void Awake()
    {
        base.Awake();
        damage = 2;
    }
    protected override void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")|| other.CompareTag("Item")|| other.CompareTag("PlayerShot" ))
        {
            return;
        }
        if (other.GetComponent<Enemy>() != null) 
        { 
            Enemy enemyScript = other.GetComponent<Enemy>();
            enemyScript.Health -= damage;
            enemyScript.Burn(2, 0.3f);
            Debug.Log(enemyScript.Health);
        }
        Destroy(gameObject);
    }
}
