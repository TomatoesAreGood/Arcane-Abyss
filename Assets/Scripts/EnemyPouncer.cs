using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPouncer : Enemy
{
    private enum State
    {
        Poucing,
        ChaseTarget,
    }

    private State state;
    private AIPath _path;
    private float targetRange = 3f;

    private bool isPouncing;
    private bool canPounce = true;
    private bool isPounceHandlerRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        state = State.ChaseTarget;
        _path = GetComponent<AIPath>();
    }



    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.ChaseTarget:
                _path.canMove = true;
                FindTarget();
                break;

            case State.Poucing:
                _path.canMove = false;
                float distance = Vector2.Distance(transform.position, Player.transform.position);
                /*Vector2 pounceVector = (Player.transform.position - transform.position);*/
                if (canPounce)
                {
                    if (!isPounceHandlerRunning)
                    {
                        StartCoroutine(PounceHandler());
                    }
                    if (!isPouncing)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, distance * 8 * Time.deltaTime);
                    }

                }

                Debug.Log("moving");
                if (distance >= targetRange && !canPounce)
                {
                    state = State.ChaseTarget;
                }
                break;
        }
        // state machine cited from Code Monkey's video "Simple Enemy AI in Unity (State Machine, Find Target, Chase, Attack)" from Youtube

    }

    private void FindTarget()
    {
        float targetRange = 2.5f;
        if (Vector2.Distance(transform.position, Player.transform.position) < targetRange)
        {
            //player is within pounching range
            Debug.Log("within range");
            state = State.Poucing;
        }
    }

    IEnumerator PounceHandler()
    {
        isPounceHandlerRunning = true;
        canPounce = true;
        yield return new WaitForSeconds(0.5f);
        isPouncing = false;
        yield return new WaitForSeconds(0.2f);
        canPounce = false;
        isPouncing = true;
        yield return new WaitForSeconds(2);
        canPounce = true;
        isPounceHandlerRunning = false;
        //function controls when the enemy can and should pounce and relays that information back to the update loop
        //ensures that the enemy is not constanly trying to pounce

        //Code cited from Unity Forum Post "2D enemy dash movement"
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("collision");
            _playerScript = collision.gameObject.GetComponent<PlayerController>();
            /*_player.GainHeart();*/
            Attack();
        }
    }
}
