using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Variables
    public float[] damagePoint = {1, 2, 3, 4, 5 };
    public float[] pushForce = {1, 2, 3, 4, 5 };

    public int weaponLevel = 0;

    private Animator _animator;
    public SpriteRenderer _spriteRenderer;

    private float coolDown = 0.5f;
    private float lastSwing;



    // start
    protected override void Start()
    {
        base.Start();

        _animator = GetComponent<Animator>();
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
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("RecieveDamage", dmg);

        } // end if

    }


    private void Swing()
    {
        _animator.SetTrigger("Swing");
    }

    /** END **/


    /** UPGRADE WEAPON **/
    public void UpgradeWeapon()
    {
        weaponLevel++;

        _spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
    /** END **/


    /** LOAD WEAPON LEVEL **/
    public void LoadWeapon(int level)
    {
        weaponLevel = level;

        _spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
    /** END **/
}
