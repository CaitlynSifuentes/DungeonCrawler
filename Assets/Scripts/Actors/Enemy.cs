using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Fighter
{
    /** VARIABLES **/
    public float speed = 200f;
    public int experience = 2;

    public Animator _animator;
    private SpriteRenderer _SpriteRenderer;
    public AIPath aiPath;

    // pathfinding //
    private Vector3 startingPosition;
    public AIDestinationSetter destinationSetter;
    private GameObject roamingObject;
    private RandomPoint randomPoint;
    private enum State
    {
        Roaming, 
        Chasing, 
        LostTarget,
    }
    private State _state;


    // chasing
    public Transform playerTarget;
    public float targetRange = 5f;

    // lost target
    public float maxTargetDistance = 15f;

    private void Awake()
    {
        _state = State.Roaming;
    }

    private void Start()
    {
        startingPosition = transform.position;
        randomPoint = GetComponentInChildren<RandomPoint>();
        roamingObject = this.gameObject.transform.GetChild(0).gameObject;
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        // if the ai has not reached end of his path then he is walking
        _animator.SetBool("isWalking", !aiPath.reachedEndOfPath);

        // flips the sprite to the correct direction
        if (aiPath.desiredVelocity.x >= 0.1f)
        {
            _SpriteRenderer.flipX = false;
        }
        else if (aiPath.desiredVelocity.x <= -0.1f)
        {
            _SpriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            default:
            case State.Roaming:

                // checks to see if target is within range
                TargetInRange();

                break;
            case State.Chasing:

                // disables the randomRoam object coroutine
                randomPoint.continueCoroutine = false;

                // sets the player as the target for the AIPath
                destinationSetter.target = playerTarget;

                // if target gets too far away from enemy
                if (Vector3.Distance(transform.position, playerTarget.position) > maxTargetDistance)
                {
                    _state = State.LostTarget;
                }

                break;
            case State.LostTarget:

                // restarts the roaming functions
                destinationSetter.target = roamingObject.transform;

                randomPoint.continueCoroutine = true;

                _state = State.Roaming;

                break;
        }
    }


    /** CHASING LOGIC **/
    private void TargetInRange()
    {
        if (Vector3.Distance(transform.position, playerTarget.position) < targetRange)
        {
            _state = State.Chasing;
        }
    }
    /** END **/



    /** DEATH **/
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.experience += experience;
        GameManager.instance.ShowText("+ " + experience + " XP", 25, new Color(255 / 255f, 179 / 255f, 25 / 255f), transform.position, Vector3.up * 40, 1f);
    }

}
