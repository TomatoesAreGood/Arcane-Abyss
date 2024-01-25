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

    public override void Fire()
    {
        if (Time.time >= nextAvailFire && PlayerController.Mana - manaCost > 0)
        {
            SoundManager.instance.PlayWindSpellSFX();

            Vector3 muzzlePos = PlayerController.Instance.firePoint.position;
            Vector2 shootDirection = PlayerController.Instance.firePoint.right;

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            var magicShot = Instantiate(spellShot, muzzlePos, Quaternion.Euler(0f, 0f, angle));

            if (PlayerController.Instance.inventory.equippedStaff != null)
            {
                magicShot.GetComponent<MagicShot>().AddDamage(PlayerController.Instance.inventory.equippedStaff.damageBonus);
            }

            magicShot.GetComponent<Rigidbody2D>().AddForce(shootDirection * speed, ForceMode2D.Impulse);

            nextAvailFire = Time.time + 1 / fireRate;
            PlayerController.Mana -= manaCost;
        }
    }
}
