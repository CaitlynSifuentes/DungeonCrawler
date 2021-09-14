using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float movementSpeed = 260;
    private Vector2 zeroMovement = new Vector2(0.0f, 0.0f);
    private Vector3 moveDelta;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        /* MOVEMENT CONTROLS */

        // getting horizontal and vertical input from user
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // reset moveDelta
        moveDelta = new Vector3(x, y, 0).normalized;

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
        }

        /* END */
    }
}
