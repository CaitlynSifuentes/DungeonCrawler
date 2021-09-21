using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    // Variables 
    public ContactFilter2D _filter;
    private BoxCollider2D _boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        // setting variables
        _boxCollider = GetComponent<BoxCollider2D>();

    }


    protected virtual void Update ()
    {
        /* COLLISION WORK */
        
        // adds collided object to array
        _boxCollider.OverlapCollider(_filter, hits);

        // loops through the array and sorts accordingly
        for (int i = 0; i < hits.Length; i++)
        {
            // if we are not colliding with object
            if(hits[i] == null)
            {
                continue;
            } // end if


            // allows child to perform operations based on its own specifications
            OnCollide(hits[i]);


            // cleaning the array
            hits[i] = null;
        }
        /* END */
    }


    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("This class has not implemented OnCollide " + this.name);
    }
}
