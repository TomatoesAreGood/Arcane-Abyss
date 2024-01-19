using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShotSpell : Spell
{
    protected override void Start(){
        nextAvailFire = Time.time;
        fireRate = 1.5f;
        manaCost = 15;
        speed = 8;
    }
}
