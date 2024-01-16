using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health;
    protected int _moveSpeed;
    protected float _timer;
    public GameObject Player;
    protected PlayerController _playerScript;
    protected SpriteRenderer _graphics;

    protected RaycastHit2D hit;
    private LayerMask _layerMask;

    protected AIPath _path;

    protected bool isMovingAway = false;
    protected bool isSlowedHandlerRunning = false;

    private float _totalBurnDamage;

    protected State state;
    protected Rigidbody2D _rigidbody;
    protected Collider2D[] _collider;
    protected ContactFilter2D _contactFilter;

    // Start is called before the first frame update

    protected enum State
    {
        Poucing,
        ChaseTarget,
        MoveAway,
        PlayerMoveAway,
        Shooting,
        Stunned,
    }
    private void Awake()
    {
        _graphics = GetComponentInChildren<SpriteRenderer>();

    }
    void Start()
    {
        Health = 5;

    }

    // Update is called once per frame
    void Update()
    {
        Destroy();
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
                if (!isSlowedHandlerRunning){
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
                // Debug.Log("enemy detected");
                state = State.MoveAway;
            }

            angle = angle + angleIncrement;
            counter--;
        }


    }

    public void DebuffSlowed()
    {
        StartCoroutine(SlowedHandler());
    }
    protected IEnumerator SlowedHandler()
    {
        isSlowedHandlerRunning = true;
        int _normalSpeed = _moveSpeed;
        _moveSpeed = _moveSpeed/2;
        _path.maxSpeed = _moveSpeed;

        Debug.Log(_moveSpeed);

        yield return new WaitForSeconds(3);
        _moveSpeed = _normalSpeed;
        _path.maxSpeed = _moveSpeed;
    }

    public void Burn(float burnDamage, float burnTickDamage)
    {
        StartCoroutine(BurnHandler(burnDamage, burnTickDamage));
    }

    protected IEnumerator BurnHandler(float burnDamage, float burnTickDamage)
    {

        _totalBurnDamage += burnDamage;
        while (_totalBurnDamage > 0)
        {
            Health -= burnTickDamage;
            _graphics.material.color = Color.yellow;
            _totalBurnDamage -= burnTickDamage;
            yield return new WaitForSeconds(1);
            _graphics.material.color = Color.white;
            yield return new WaitForSeconds(1);


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

    protected void Destroy()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
