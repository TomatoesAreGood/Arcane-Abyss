using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Health = 8;
        _moveSpeed = 2.5f;
        Player = PlayerController.instance.gameObject;
        _path.maxSpeed = _moveSpeed;
    }

    // Update is called once per frame


    protected override void FixedUpdate()
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

                //if the direction is zero(edge case), select a random direction
                if (dir.x == 0 && dir.y == 0)
                {
                    dir.x = Random.Range(1, 2);
                    dir.y = Random.Range(1, 2);
                }

                //move enemy away in opposite direction of direction to player
                _rigidbody.MovePosition(_rigidbody.position + dir * _moveSpeed * Time.fixedDeltaTime);
                if (Vector2.Distance(transform.position, hit.point) > 2.5f)
                {
                    state = State.ChaseTarget;
                }
                break;

        }
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
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
