using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBossWeapon : Collidable
{
    /** VARIABLES **/
    public float damage;
    public float pushForce;
    public Animator _animatorBoss;

    private float timeBetweenDamage = 3.0f;
    private float lastDamageDone;

    private float biteWaitTime = 10.0f;
    private float lastBiteTime;


    protected override void Start()
    {
        base.Start();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (Time.time - lastDamageDone > timeBetweenDamage)
            {
                lastDamageDone = Time.time;

                // creating damage object to send to the fighter that was hit
                Damage dmg = new Damage
                {
                    damageAmount = damage,
                    origin = transform.position,
                    pushForce = pushForce
                };

                coll.SendMessage("RecieveDamage", dmg);

                GameManager.instance.PlayerDamaged();

                if (Time.time - lastBiteTime > biteWaitTime)
                {
                    lastBiteTime = Time.time;

                    _animatorBoss.SetBool("isChomping", true);

                    GameManager.instance.playerScript.movementSpeed = 0;

                    StartCoroutine(ChompOnPlayer(3.0f));
                } // end if
            } // end if
        }
    }


    private IEnumerator ChompOnPlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);


        GameManager.instance.playerScript.movementSpeed = 260;

        _animatorBoss.SetBool("isChomping", false);
    }
}
