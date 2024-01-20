using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{
    protected Animator animator;
    protected float damage;

    protected virtual void Awake(){
        damage = 1; 
        animator = gameObject.GetComponent<Animator>();  
    }

    protected virtual void Update(){
        Vector2 characterPos = PlayerController.instance.transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(characterPos.x - transform.position.x, 2) + Mathf.Pow(characterPos.y - transform.position.y, 2));
        if (distance > 35){
            Destroy(gameObject);
        }
        
    }
    
    public void AddDamage(float dmg){
        this.damage += dmg;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player") || other.CompareTag("Item"))
        {
            return;
        }
        if (other.GetComponent<Enemy>() != null) 
        { 
            Enemy enemyScript = other.GetComponent<Enemy>();
            enemyScript.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    
}
