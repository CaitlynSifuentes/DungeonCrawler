using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    // variables
    protected bool collected = false;

    // check to ensure that the player is what hit the object
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
            OnCollect();
    }


    protected virtual void OnCollect()
    {
        collected = true;
    }
}
