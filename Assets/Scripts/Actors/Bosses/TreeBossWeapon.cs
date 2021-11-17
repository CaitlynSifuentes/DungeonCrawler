using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBossWeapon : Collidable
{
    /** VARIABLES **/
    public float damage;
    public float pushForce;
    public Animator _animatorBoss;
    public TreeBoss treeBossScript;

    public AudioSource weaponSoundSource;
    public AudioClip slam;

    private float timeBetweenDamage = 2f;
    private float lastDamageDone;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (Time.time - lastDamageDone > timeBetweenDamage)
            {
                lastDamageDone = Time.time;

                // pushes the player backwards and stops their movement
                var magnitude = 7000;

                var force = transform.position - coll.transform.position;

                force.Normalize();
                GameManager.instance.playerScript._rigidbody2D.AddForce(-force * magnitude);

                GameManager.instance.playerScript.movementSpeed = 0;
                StartCoroutine(StunnedPlayer(.6f));

                // creating damage object to send to the fighter that was hit
                Damage dmg = new Damage
                {
                    damageAmount = damage,
                    origin = transform.position,
                    pushForce = pushForce
                };

                coll.SendMessage("RecieveDamage", dmg);

                GameManager.instance.PlayerDamaged();

                // shakes the camera
                ScreenShakeController.instance.StartShake(.2f, .2f);

                // play sound
                if (!weaponSoundSource.isPlaying)
                    weaponSoundSource.PlayOneShot(slam);

                treeBossScript.SlammedPlayer();

            } // end if
        }
    }


    // after waitTime player can move again
    private IEnumerator StunnedPlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);


        GameManager.instance.playerScript.movementSpeed = 260;
    }
}