using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPouncer : Enemy
{

    private int _pounceSpeed;
    private Animator _animator;
    private bool _isPouncing;
    private bool _canPounce = true;
    private bool _isPounceHandlerRunning = false;

    // Start is called before the first frame update
    protected override void Awake(){
        base.Awake();
        EnemyState = State.ChaseTarget;
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }
    protected override void Start()
    {
        Player = PlayerController.instance.gameObject;
        Health = 4;
        _pounceSpeed = 8;
        _moveSpeed = (int)_path.maxSpeed;
        EnemyID = 1;
        StartCoroutine(SlowedHandler(1));
    }

    protected override void FixedUpdate()
    {
       // Debug.Log(gameObject.name + EnemyState);
        Vector2 pos = transform.position;
        switch (EnemyState)
        {
            default:
            case State.ChaseTarget:
                _path.canMove = true;
                FindTarget();
                FindEnemy();
                break;

            case State.Poucing:
                _path.canMove = false;
                Vector2 pounceDir = Player.transform.position - transform.position;
                float distance = Vector2.Distance(transform.position, Player.transform.position);
                if (_canPounce)
                {
                    if (!_isPounceHandlerRunning)
                    {
                        StartCoroutine(PounceHandler());
                    }
                    if (!_isPouncing)
                    {
                        _rigidbody.MovePosition(_rigidbody.position + pounceDir * 8 *  Time.fixedDeltaTime); 

                    }

                }

                if (distance >= 2.5 && !_canPounce)
                {
                    EnemyState = State.ChaseTarget;
                }
                break;
            // state machine cited from Code Monkey's video "Simple Enemy AI in Unity (State Machine, Find Target, Chase, Attack)" from Youtube

            case State.MoveAway:
                _path.canMove = false;
                Vector2 dir = -(_hit.point - pos);

                //if the direction is zero(edge case), select a random direction
                if (dir.x == 0 && dir.y == 0)
                {
                    dir.x = Random.Range(1, 2);
                    dir.y = Random.Range(1, 2);
                }

                //move enemy away in opposite direction of direction to player
                _rigidbody.MovePosition(_rigidbody.position + dir * _moveSpeed * Time.fixedDeltaTime);
                if (Vector2.Distance(transform.position, _hit.point) > 2.5f)
                {
                    EnemyState = State.ChaseTarget;
                }
                break;
            case State.Stunned:
                _path.canMove = false;
                _timer += Time.fixedDeltaTime;
                if (_timer > 1)
                {
                    EnemyState = State.ChaseTarget;
                }
                _timer = 0;
                break;
        }
        if(EnemyState == State.Poucing){
            _animator.SetBool("IsPouncing", true);
        }else{
            _animator.SetBool("IsPouncing", false);
        }
    }


    /*    private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 2.5f);
        }
    */

    private IEnumerator PounceHandler()
    {
        _isPounceHandlerRunning = true;
        _canPounce = true;
        yield return new WaitForSeconds(0.5f);
        _isPouncing = false;
        yield return new WaitForSeconds(0.2f);
        _canPounce = false;
        _isPouncing = true;
        yield return new WaitForSeconds(2);
        _canPounce = true;
        _isPounceHandlerRunning = false;
        //function controls when the enemy can and should pounce and relays that information back to the update loop
        //ensures that the enemy is not constanly trying to pounce

        //Code cited from Unity Forum Post "2D enemy dash movement"

    }


}
