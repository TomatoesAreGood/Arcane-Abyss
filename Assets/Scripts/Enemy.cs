using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int _damage;
    protected int _speed;
    public GameObject Player;
    protected PlayerController _playerScript;
    protected RaycastHit2D hit;
    private LayerMask _layerMask;

    protected AIPath _path;
    protected float targetRange;

    protected bool isPouncing;
    protected bool canPounce = true;
    protected bool isPounceHandlerRunning = false;

    protected State state;
    protected Collider2D[] _collider;
    protected ContactFilter2D _contactFilter;

    // Start is called before the first frame update

    protected enum State
    {
        Poucing,
        ChaseTarget,
        MoveAway,
    }
    void Start()
    {

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

            case State.MoveAway:
                _path.canMove = false;

                break;
        }


    }
    protected void FindTarget()
    {
        float targetRange = 2.5f;
        if (Vector2.Distance(transform.position, Player.transform.position) < targetRange)
        {
            //player is within pounching range
            Debug.Log("within range");
            state = State.Poucing;
        }
    }

    protected void FindEnemy()
    {
        int counter = 10;
        int angleIncrement = 360 / counter;
        int angle = 0;
        _layerMask = LayerMask.GetMask("Enemies");
        while (counter > 0)
        {
            var dir = new Vector2(Mathf.Sin(angle) + transform.position.x, Mathf.Cos(angle) + transform.position.y);
            hit = Physics2D.Raycast(transform.position, dir, 2.5f, _layerMask);
            Debug.DrawLine(transform.position, dir);

            if (hit.collider != null)
            {
                state = State.MoveAway;
                Debug.Log(hit.collider);
            }

            angle = angle + angleIncrement;
            counter--;
        }


    }



    protected void Attack()
    {
        _playerScript.TakeDamage(1);

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
