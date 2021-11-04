using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingWell : Collidable
{
    // Variables
    public int healingAmount = 1;

    private float healingCoolDown = 1f;
    private float lastHealed;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (Time.time - lastHealed > healingCoolDown)
            {
                lastHealed = Time.time;
                GameManager.instance.playerScript.Heal(healingAmount);

            } // end if

        } // end if
    }
}
