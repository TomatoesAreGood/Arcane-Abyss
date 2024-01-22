using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health;
    protected float _moveSpeed;
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

    public State state;
    protected Rigidbody2D _rigidbody;
    protected Collider2D[] _collider;
    protected ContactFilter2D _contactFilter;

    // Start is called before the first frame update

    public enum State
    {
        Poucing,
        ChaseTarget,
        MoveAway,
        PlayerMoveAway,
        Shooting,
        Stunned,
    }

    public static Vector2 AsVector2(Vector3 vec)
    {
        return new Vector2 (vec.x, vec.y);
    }
    protected virtual void Awake()
    {
        _graphics = GetComponentInChildren<SpriteRenderer>();
        _path = GetComponent<AIPath>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        Player = PlayerController.instance.gameObject;
        _moveSpeed = 3;
        StartCoroutine(SlowedHandler(1));
        Health = 5;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Health <= 0)
        {
            FinalStats.enemies.Add(gameObject.name);
            Destroy(gameObject);
        }
    }

    protected virtual void FixedUpdate()
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
            case State.Stunned:
                _path.canMove = false;
                _timer += Time.fixedDeltaTime;
                if (_timer > 1)
                {
                    state = State.ChaseTarget;
                    _timer = 0;

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
            //Debug.Log("within range");
            state = State.Poucing;
        }
    }

    protected void FindEnemy()
    {
        int counter = 10;
        int angleIncrement = 360 / counter;
        int angle = 0;
        while (counter > 0)
        {
            var dir = new Vector2(Mathf.Sin(angle) + transform.position.x, Mathf.Cos(angle) + transform.position.y);
            hit = Physics2D.Raycast(transform.position, dir, 0.5f);
            Debug.DrawLine(transform.position, dir);



            angle = angle + angleIncrement;
            counter--;
        }

        if (hit.collider.gameObject != gameObject && hit.collider.CompareTag("Enemy"))
        {
            Debug.Log("enemy detected");
            state = State.MoveAway;
        }

    }

    public void DebuffSlowed()
    {
        if(!isSlowedHandlerRunning){
            StartCoroutine(SlowedHandler());
        }
    }
    protected IEnumerator SlowedHandler()
    {
        isSlowedHandlerRunning = true;
        _path.maxSpeed = _moveSpeed/2;
        yield return new WaitForSeconds(3);
        _path.maxSpeed = _moveSpeed;
        isSlowedHandlerRunning = false;

    }

    protected IEnumerator SlowedHandler(int time)
    {
        isSlowedHandlerRunning = true;
        _path.maxSpeed = _moveSpeed/2;
        yield return new WaitForSeconds(time);
        _path.maxSpeed = _moveSpeed;
        isSlowedHandlerRunning   = false;
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

    public void TakeDamage(float num){
        Health -= num;
    }




    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
/*        Debug.Log("collision");
*/
        if (collision.CompareTag("PlayerIsTrigger"))
        {
            Debug.Log("collision");
            _playerScript = PlayerController.instance.gameObject.GetComponent<PlayerController>();
            /*_player.GainHeart();*/
            Attack();
        }
    }

   


}
