using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Collidable
{
    /** VARIABLES **/
    public int damage;
    public float pushForce;
    private Animator _animator;

    protected override void Start()
    {
        base.Start();

        _animator = GetComponent<Animator>();
    }

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

            _animator.SetTrigger("Swing");
        }
    }
}
