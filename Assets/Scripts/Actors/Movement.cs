using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : Fighter
{
    // Variables
    public float movementSpeed = 260;

    protected float ySpeed = 200f;
    protected float xSpeed = 200f;

    protected Vector2 zeroMovement = new Vector2(0.0f, 0.0f);
    protected Vector3 moveDelta;
    protected Rigidbody2D _rigidbody2D;
    protected Animator _animator;



    protected virtual void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }



    /* PLAYER MOVEMENT CONTROLS */
    protected virtual void UpdateMotor(Vector3 input)
    {

        // reset moveDelta
        moveDelta = input.normalized;


        // change sprite direction depending on input 
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } // end if


        // moves player forward
        _rigidbody2D.velocity = moveDelta * Time.deltaTime * movementSpeed;

        if (_rigidbody2D.velocity == zeroMovement)
        {
            _animator.SetBool("isWalking", false);
        }
        else
        {
            _animator.SetBool("isWalking", true);
        } // end if
    }
    /* END */

}
