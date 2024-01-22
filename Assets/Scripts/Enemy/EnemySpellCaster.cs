using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellCaster : EnemyRanger
{
    protected override void Start(){
        base.Start();
        Health = 7;
        _moveSpeed = 3f;
        damage = 2;
        fireRate = 0.6f;
        projectileSpeed = 3;
        enemyID = 2;
    }

   protected override void FixedUpdate()
    {
        base.FixedUpdate();
        

    }
    
}
