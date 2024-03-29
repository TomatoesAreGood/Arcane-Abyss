using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected int damage;

    protected virtual void Start(){
        // damage = 1; 
        if(GetComponent<Animator>() != null){
            animator = GetComponent<Animator>();
        }
        rb = GetComponent<Rigidbody2D>();
          
    }

    protected virtual void Update(){
        Vector2 characterPos = transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(characterPos.x - transform.position.x, 2) + Mathf.Pow(characterPos.y - transform.position.y, 2));
        if (distance > 35){
            Destroy(gameObject);
        }
        
    }
    
    public void SetDamage(int dmg){
        damage = dmg;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy") || other.CompareTag("Item")){
            return;
        }
        if (other.CompareTag("PlayerIsTrigger") || other.CompareTag("Player")) 
        { 
            PlayerController.Instance.TakeDamage(damage);
        }
        if(animator != null){
            animator.SetBool("OnDestroy", true);
            rb.velocity = Vector2.zero;
            Destroy(gameObject, 0.3f);
        }else{
            Destroy(gameObject);
        }
    }

}
