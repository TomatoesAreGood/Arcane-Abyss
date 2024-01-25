using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour, IEquatable<Spell>
{
    public int ManaCost;
    public float FireRate;
    public float Speed;
    [SerializeField] public GameObject SpellShot;
    public float NextAvailFire;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        NextAvailFire = Time.time;
        FireRate = 2;
        ManaCost = 5;
        Speed = 15;
    }

  
    public virtual void Fire() {
        if(Time.time >= NextAvailFire && PlayerController.Mana - ManaCost > 0){
            Vector3 muzzlePos = PlayerController.Instance.firePoint.position;
            Vector2 shootDirection = PlayerController.Instance.firePoint.right;

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            var magicShot = Instantiate(SpellShot, muzzlePos, Quaternion.Euler(0f, 0f, angle));

            if(PlayerController.Instance.inventory.equippedStaff != null){
                magicShot.GetComponent<MagicShot>().AddDamage(PlayerController.Instance.inventory.equippedStaff.damageBonus);
            }

            magicShot.GetComponent<Rigidbody2D>().AddForce(shootDirection * Speed, ForceMode2D.Impulse);

            NextAvailFire = Time.time + 1/FireRate;
            PlayerController.Mana -= ManaCost;
        }
    }

    public bool Equals(Spell other)
    {
        // Would still want to check for null etc. first.
        return this.SpellShot == other.SpellShot; 
             
    }
}
