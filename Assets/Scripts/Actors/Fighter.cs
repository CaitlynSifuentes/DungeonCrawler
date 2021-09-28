using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Variables
    public float hitPoints = 2;
    public float maxHitPoint = 2;

    public float pushRecoverySpeed = 0.2f;

    protected float immuneTime = 1.0f;
    protected float lastImmune;

    protected Vector3 pushDirection;


    /** DAMAGE HANDLING **/
    protected virtual void RecieveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoints -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            // shows the damage amount on screen
            GameManager.instance.ShowText("- "+ dmg.damageAmount.ToString(), 25, Color.red, 
                new Vector3(transform.position.x, transform.position.y + 1, 0), Vector3.zero, 1f);

            // if damage kills player
            if (hitPoints <= 0)
            {
                hitPoints = 0;
                Death();

            } //end if
        } // end if
    }

    protected virtual void Death()
    {
        Debug.Log(this.name + " has died!");
    }
    /** END **/
}
