using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{
    protected int damage;

    protected virtual void Start(){
        damage = 1;   
    }

    protected virtual void Update(){
        Vector2 characterPos = PlayerController.instance.transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(characterPos.x - transform.position.x, 2) + Mathf.Pow(characterPos.y - transform.position.y, 2));
        if (distance > 35){
            Destroy(gameObject);
        }
    }
    
    public void AddDamage(int dmg){
        damage += dmg;
    }

    protected void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            return;
        }
        Destroy(gameObject);
    }

    
}
