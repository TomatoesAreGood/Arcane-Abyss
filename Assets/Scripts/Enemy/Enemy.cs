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

    protected RaycastHit2D _hit;
    private LayerMask _layerMask;

    protected AIPath _path;

    protected bool _isMovingAway = false;
    protected bool _isSlowedHandlerRunning = false;

    private float _totalBurnDamage;

    public State EnemyState;
    protected Rigidbody2D _rigidbody;
    protected Collider2D[] _collider;
    protected ContactFilter2D _contactFilter;
    public int EnemyID;

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
        EnemySaveManager.instance.allEnemies.Add(this);
    }
    protected void OnDisable(){
        EnemySaveManager.instance.allEnemies.Remove(this);
    }

    protected virtual void Start()
    {
        Player = PlayerController.instance.gameObject;
        _moveSpeed = 3;
        StartCoroutine(SlowedHandler(1));
        Health = 5;
        EnemyID = 0;
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
            switch (EnemyState)
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
            EnemyState = State.Poucing;
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
            _hit = Physics2D.Raycast(transform.position, dir, 0.5f);
            Debug.DrawLine(transform.position, dir);



            angle = angle + angleIncrement;
            counter--;
        }
        if(!_hit){
            return;
        }

        if (_hit.collider.gameObject != gameObject && _hit.collider.CompareTag("Enemy"))
        {
            //Debug.Log("enemy detected");
            EnemyState = State.MoveAway;
        }

    }

    public void DebuffSlowed()
    {
        if(!_isSlowedHandlerRunning){
            StartCoroutine(SlowedHandler());
        }
    }
    protected IEnumerator SlowedHandler()
    {
        _isSlowedHandlerRunning = true;
        _path.maxSpeed = _moveSpeed/2;
        yield return new WaitForSeconds(3);
        _path.maxSpeed = _moveSpeed;
        _isSlowedHandlerRunning = false;

    }

    protected IEnumerator SlowedHandler(int time)
    {
        _isSlowedHandlerRunning = true;
        _path.maxSpeed = _moveSpeed/2;
        yield return new WaitForSeconds(time);
        _path.maxSpeed = _moveSpeed;
        _isSlowedHandlerRunning   = false;
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
        SoundManager.instance.PlayPlayerDamageSFX();
    }

    public void TakeDamage(float num){
        Health -= num;
        SoundManager.instance.PlayEnemyDamageSFX();
    }




    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
/*        Debug.Log("collision");
*/
        if (collision.CompareTag("PlayerIsTrigger"))
        {
            //Debug.Log("collision");
            _playerScript = PlayerController.instance.gameObject.GetComponent<PlayerController>();
            /*_player.GainHeart();*/
            Attack();
        }
    }

   


}
