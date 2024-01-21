using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour, IEquatable<Spell>
{
    public int manaCost;
    public float fireRate;
    public float speed;
    [SerializeField] public GameObject spellShot;
    public float nextAvailFire;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        nextAvailFire = Time.time;
        fireRate = 2;
        manaCost = 5;
        speed = 15;
    }

  
    public virtual void Fire() {
        if(Time.time >= nextAvailFire && PlayerController.mana - manaCost > 0){
            Vector3 muzzlePos = PlayerController.instance.firePoint.position;
            Vector2 shootDirection = PlayerController.instance.firePoint.right;

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            var magicShot = Instantiate(spellShot, muzzlePos, Quaternion.Euler(0f, 0f, angle));

            if(PlayerController.instance.inventory.equippedStaff != null){
                magicShot.GetComponent<MagicShot>().AddDamage(PlayerController.instance.inventory.equippedStaff.damageBonus);
            }

            magicShot.GetComponent<Rigidbody2D>().AddForce(shootDirection * speed, ForceMode2D.Impulse);

            nextAvailFire = Time.time + 1/fireRate;
            PlayerController.mana -= manaCost;
        }
    }

    public bool Equals(Spell other)
    {
        // Would still want to check for null etc. first.
        return this.spellShot == other.spellShot; 
             
    }
}
