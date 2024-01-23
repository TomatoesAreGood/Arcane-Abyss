using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellCaster : EnemyRanger
{
    protected override void Start(){
        base.Start();
        Health = 7;
        _moveSpeed = 3f;
        _damage = 2;
        _fireRate = 0.6f;
        _projectileSpeed = 3;
        EnemyID = 2;
    }

   protected override void FixedUpdate()
    {
        base.FixedUpdate();
        

    }
    
}
