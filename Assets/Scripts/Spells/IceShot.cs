using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class IceShot : MagicShot
{

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        damage = 2;
    }
   protected override void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player") || other.CompareTag("Item"))
        {
            return;
        }
        if (other.GetComponent<Enemy>() != null) 
        { 
            Enemy enemyScript = other.GetComponent<Enemy>();
            enemyScript.Health -= damage;
            enemyScript.DebuffSlowed();
        }        
        animator.SetBool("OnDestroy", true);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(gameObject, 0.25f);
    }

}
