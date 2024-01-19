using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotSpell : Spell
{
    protected override void Start(){
        nextAvailFire = Time.time;
        fireRate = 1.5f;
        manaCost = 10;
        speed = 10;
    }
}
