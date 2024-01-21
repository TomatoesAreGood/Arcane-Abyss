using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindShot : MagicShot
{
    protected override void Awake(){
        base.Awake();
        damage = 2;
    }

   protected override void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")|| other.CompareTag("Item") || other.CompareTag("PlayerShot"))
        {
            return;
        }
        if (other.GetComponent<Enemy>() != null) 
        { 
            Enemy enemyScript = other.GetComponent<Enemy>();
            enemyScript.Health -= damage;
            enemyScript.state = Enemy.State.Stunned;
        }        
        animator.SetBool("OnDestroy", true);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(gameObject, 0.25f);
    }
}
