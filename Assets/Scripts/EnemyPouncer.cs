using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPouncer : Enemy
{

    private int _pounceSpeed;
    
    private bool isPouncing;
    private bool canPounce = true;
    private bool isPounceHandlerRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        Health = 4;
        _path = GetComponent<AIPath>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _pounceSpeed = 8;
        _moveSpeed = (int)_path.maxSpeed;
    }
    void Update()
    {
        Destroy();
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        switch (state)
        {
            default:
            case State.ChaseTarget:
                _path.canMove = true;
                FindTarget();
                FindEnemy();
                if (!isSlowedHandlerRunning)
                {
                    StartCoroutine(SlowedHandler());
                }
                break;

            case State.Poucing:
                _path.canMove = false;
                Vector2 pounceDir = Player.transform.position - transform.position;
                float distance = Vector2.Distance(transform.position, Player.transform.position);
                if (canPounce)
                {
                    if (!isPounceHandlerRunning)
                    {
                        StartCoroutine(PounceHandler());
                    }
                    if (!isPouncing)
                    {
                        _rigidbody.MovePosition(_rigidbody.position + pounceDir * 8 *  Time.fixedDeltaTime); 

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
                Vector2 dir = -(hit.point - pos);


                _rigidbody.MovePosition(_rigidbody.position + dir * _moveSpeed * Time.fixedDeltaTime);
                if ((Vector2.Distance(transform.position, hit.point) > 2.5f))
                {
                    state = State.ChaseTarget;
                }
                break;
            case State.Stunned:
                _path.canMove = false;
                _timer += Time.fixedDeltaTime;
                if (_timer > 3)
                {
                    state = State.ChaseTarget;
                }
                break;
        }
    }

    // Update is called once per frame





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
