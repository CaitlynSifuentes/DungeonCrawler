using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Variables
    public int damagePoint = 1;
    public float pushForce = 1.0f;
    public int weaponLevel = 0;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float coolDown = 0.5f;
    private float lastSwing;



    // start
    protected override void Start()
    {
        base.Start();

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }



    // update
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (Time.time - lastSwing > coolDown)
            {
                lastSwing = Time.time;
                Swing();
            } // end if
        } // end if 
    }



    /** SWING LOGIC **/
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            // creating damage object to send to the fighter that was hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("RecieveDamage", dmg);

        } // end if

    }


    private void Swing()
    {
        _animator.SetTrigger("Swing");
    }
    /** END **/
}
