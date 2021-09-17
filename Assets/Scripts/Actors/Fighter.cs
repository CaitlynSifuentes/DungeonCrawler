using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Variables
    public int hitPoints = 10;
    public int maxHitPoint = 10;
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
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), (int)Random.Range(20, 30), Color.red,
                transform.position, Vector3.zero, 1f);

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
