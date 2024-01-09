using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class IceShot : MagicShot
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        damage = 2;
    }
   protected override void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {
            return;
        }
        animator.SetBool("OnDestroy", true);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(gameObject, 0.25f);
    }

}
