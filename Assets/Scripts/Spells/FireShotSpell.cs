using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotSpell : Spell
{
    protected override void Start(){
        NextAvailFire = Time.time;
        FireRate = 1.5f;
        ManaCost = 10;
        Speed = 10;
    }
    public override void Fire() {
        if (Time.time >= NextAvailFire && PlayerController.Mana - ManaCost > 0)
        {
            SoundManager.instance.PlayFireBallSFX();

            Vector3 muzzlePos = PlayerController.Instance.firePoint.position;
            Vector2 shootDirection = PlayerController.Instance.firePoint.right;

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            var magicShot = Instantiate(SpellShot, muzzlePos, Quaternion.Euler(0f, 0f, angle));

            if (PlayerController.Instance.inventory.equippedStaff != null)
            {
                magicShot.GetComponent<MagicShot>().AddDamage(PlayerController.Instance.inventory.equippedStaff.damageBonus);
            }

            magicShot.GetComponent<Rigidbody2D>().AddForce(shootDirection * Speed, ForceMode2D.Impulse);

            NextAvailFire = Time.time + 1 / FireRate;
            PlayerController.Mana -= ManaCost;
        }
    }
}
