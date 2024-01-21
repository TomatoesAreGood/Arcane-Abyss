using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindShotSpell : Spell
{
    protected override void Start(){
        nextAvailFire = Time.time;
        fireRate = 3f;
        manaCost = 5;
        speed = 10;
    }
}
