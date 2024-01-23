using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShotSpell : Spell
{
    public override void Fire()
    {
        if (Time.time >= nextAvailFire && PlayerController.mana - manaCost > 0)
        {
            SoundManager.instance.PlayMagicShotSFX();

            Vector3 muzzlePos = PlayerController.instance.firePoint.position;
            Vector2 shootDirection = PlayerController.instance.firePoint.right;

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            var magicShot = Instantiate(spellShot, muzzlePos, Quaternion.Euler(0f, 0f, angle));

            if (PlayerController.instance.inventory.equippedStaff != null)
            {
                magicShot.GetComponent<MagicShot>().AddDamage(PlayerController.instance.inventory.equippedStaff.damageBonus);
            }

            magicShot.GetComponent<Rigidbody2D>().AddForce(shootDirection * speed, ForceMode2D.Impulse);

            nextAvailFire = Time.time + 1 / fireRate;
            PlayerController.mana -= manaCost;
        }
    }
}
