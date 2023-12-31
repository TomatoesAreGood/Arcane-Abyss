using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int manaCost;
    public float fireRate;
    public float speed;
    [SerializeField] GameObject spellShot;
    protected float nextAvailFire;

    // Start is called before the first frame update
    void Start()
    {
        nextAvailFire = Time.time;
        fireRate = 2;
        speed = 10;
    }
    public virtual void Fire() {
        if(Time.time >= nextAvailFire){
            Vector3 muzzlePos = PlayerController.instance.firePoint.position;
            Vector2 shootDirection = PlayerController.instance.firePoint.right;

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            var magicShot = Instantiate(spellShot, muzzlePos, Quaternion.Euler(0f, 0f, angle));
            magicShot.GetComponent<MagicShot>().AddDamage(PlayerController.instance.equippedStaff.damageBonus);
            magicShot.GetComponent<Rigidbody2D>().AddForce(shootDirection * speed, ForceMode2D.Impulse);

            nextAvailFire = Time.time + 1/fireRate;
        }
    }
}
