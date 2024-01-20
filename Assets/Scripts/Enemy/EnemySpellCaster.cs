using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellCaster : EnemyRanger
{
    protected Animator animator;
    protected override void Start(){
        Health = 7;
        Player = PlayerController.instance.gameObject;
        animator = transform.GetChild(0).GetComponent<Animator>();
        state = State.ChaseTarget;
        nextAvailFire = Time.time;
        _moveSpeed = 3f;
        damage = 2;
        fireRate = 0.6f;
    }

   protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(state == State.Shooting){
            animator.SetBool("IsShooting", true);
        }else{
            animator.SetBool("IsShooting", false);
        }


    }
    
}
