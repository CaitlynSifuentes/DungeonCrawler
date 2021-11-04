using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNoWeapon : Collidable
{
    /** VARIABLES **/
    public float damage;
    public float pushForce;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            // creating damage object to send to the fighter that was hit
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("RecieveDamage", dmg);

            GameManager.instance.PlayerDamaged();
        }
    }
}
