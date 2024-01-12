using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPouncer : Enemy
{


    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        state = State.ChaseTarget;
        _path = GetComponent<AIPath>();
        moveSpeed = _path.maxSpeed;
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
                FindEnemy();
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

                if (distance >= 2.5 && !canPounce)
                {
                    state = State.ChaseTarget;
                }
                break;
            // state machine cited from Code Monkey's video "Simple Enemy AI in Unity (State Machine, Find Target, Chase, Attack)" from Youtube

            case State.MoveAway:
                _path.canMove = false;
                if (Vector2.Distance(transform.position, hit.point) < 1)
                {
                    transform.position = Vector2.MoveTowards(transform.position, hit.point.normalized, -Time.deltaTime);
                }
                transform.position = Vector2.MoveTowards(transform.position, hit.point, -3f * Time.deltaTime);
                if (Vector2.Distance(transform.position, hit.point) > 2.5f)
                {
                    state = State.ChaseTarget;
                }
                break;
        }

    }



    /*    private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 2.5f);
        }
    */

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
