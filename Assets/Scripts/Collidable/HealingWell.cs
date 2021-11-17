using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWell : Collidable
{
    // Variables
    public float healingAmount = 1;
    public float maxHealingAmount = 5;

    private float healingCoolDown = 1f;
    private float lastHealed;

    public Animator _animator;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (Time.time - lastHealed > healingCoolDown)
            {
                lastHealed = Time.time;
                _animator.SetBool("isEmpty", GameManager.instance.playerScript.Heal(healingAmount, maxHealingAmount));

            } // end if

        } // end if
    }
}
