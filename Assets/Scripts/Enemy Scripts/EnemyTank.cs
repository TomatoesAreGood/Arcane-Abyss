using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        Health = 8;
        _moveSpeed = 1.5f;
        _path = GetComponent<AIPath>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _path.maxSpeed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        DeadCheck();
    }

    private void FixedUpdate()
    {
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

            case State.MoveAway:
                _path.canMove = false;
                Vector2 pos = transform.position;
                Vector2 dir = -(hit.point - pos);
                _rigidbody.MovePosition(_rigidbody.position + dir * _moveSpeed * Time.fixedDeltaTime);
                if ((Vector2.Distance(transform.position, hit.point) > 2.5f))
                {
                    state = State.ChaseTarget;
                }
                break;

        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("collision");
            _playerScript = collision.gameObject.GetComponent<PlayerController>();
            /*_player.GainHeart();*/
            Attack();
            Attack();
        }
    }
}
