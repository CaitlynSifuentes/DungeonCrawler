using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TreeBoss : Fighter
{
    /** VARIABLES **/
    public int experience = 12;
    public float bossSpeed = 2;
    public float waitTime = 2;

    public Animator _animator;
    public AIPath aiPath;

    // pathfinding //
    public AIDestinationSetter destinationSetter;
    public GameObject randomGameObject;
    private RandomPoint randomPoint;
    private enum State
    {
        Roaming,
        Chasing,
        LostTarget,
    }
    private State _state;


    // chasing
    private Transform playerTarget;
    public float targetRange = 5f;

    // lost target
    public float maxTargetDistance = 15f;

    private void Awake()
    {
        _state = State.Roaming;
    }

    private void Start()
    {
        playerTarget = GameManager.instance.player.transform;
        randomPoint = randomGameObject.GetComponent<RandomPoint>();
        _animator.SetBool("isWalking", true);
    }

    private void Update()
    {

        // flips the sprite to the correct direction
        if (aiPath.desiredVelocity.x >= 0.1f)
        {
            transform.localScale = Vector3.one;
        }
        else if (aiPath.desiredVelocity.x <= -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
                destinationSetter.target = randomPoint.randomTransform;

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

    public void SlammedPlayer()
    {
        aiPath.maxSpeed = 0;
        _animator.SetBool("isWalking", false);
        StartCoroutine(SlamPlayer(waitTime));
    }

    private IEnumerator SlamPlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        aiPath.maxSpeed = bossSpeed;
        _animator.SetBool("isWalking", true);
    }

    /** END **/



    /** DEATH **/
    protected override void Death()
    {
        GameManager.instance.playerScript.movementSpeed = 260;
        Destroy(gameObject);
        GameManager.instance.GiveExperience(experience);
        GameManager.instance.ShowText("+ " + experience + " XP", 25, new Color(255 / 255f, 179 / 255f, 25 / 255f), new Vector3(transform.position.x, transform.position.y + 1, 0), Vector3.up * 40, 1f);
    }
    /** END **/
}