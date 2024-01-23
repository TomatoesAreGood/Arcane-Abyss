using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyRanger : Enemy
{
    public GameObject EnemyShot;
    private Rigidbody2D _shotBody;
    protected float _nextAvailFire;
    protected float _fireRate;
    protected int _damage;
    protected float _projectileSpeed;
    private Vector2 _initialArm;
    private Vector2 _nextArm;
    protected Animator _animator;
    protected RaycastHit2D _hit1;


    // Start is called before the first frame update
    protected override void Start()
    {
        Health = 5;
        Player = PlayerController.instance.gameObject;
        EnemyState = State.ChaseTarget;
        _nextAvailFire = Time.time;
        _moveSpeed = 2.5f;
        _damage = 1;
        _fireRate = 0.3f;
        _projectileSpeed = 6f;
        _animator = transform.GetChild(0).GetComponent<Animator>();
        EnemyID = 3;
    }

    // Update is called once per frame

    protected override void FixedUpdate()
    {
        LayerMask mask = LayerMask.GetMask("Obstacle");
        Vector2 losDir = Player.transform.position - transform.position;
        var dis = Vector2.Distance(Player.transform.position, transform.position);
        _hit1 = Physics2D.CircleCast(AsVector2(transform.position) + losDir.normalized, 1,  losDir, dis, mask);
       /* hit2 = Physics2D.Raycast(AsVector2(transform.position) + Vector2.Perpendicular(transform.position).normalized + losDir.normalized, losDir + Vector2.Perpendicular(transform.position).normalized, dis);
        hit3 = Physics2D.Raycast(AsVector2(transform.position) + -Vector2.Perpendicular(transform.position).normalized + losDir.normalized, losDir + Vector2.Perpendicular(transform.position).normalized, dis);*/
        /*Debug.DrawLine(transform.position, losDir);
        Debug.DrawLine(AsVector2(transform.position) + Vector2.Perpendicular(transform.position).normalized, losDir + Vector2.Perpendicular(transform.position).normalized);
        Debug.DrawLine(AsVector2(transform.position) + -Vector2.Perpendicular(transform.position).normalized + losDir.normalized, losDir + -Vector2.Perpendicular(transform.position).normalized);*/

        //Debug.Log(state);

        switch (EnemyState)
        {
            default:
            case State.ChaseTarget:
                _path.canMove = true;

                //if there is no obstacle blocking Line Of Sight of circle cast, find target
                if (_hit1.collider == null)
                {
/*                    Debug.Log(_hit1.collider);
*/                    FindTargetRanger();
                }
               
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

            case State.Shooting:
                _path.canMove = false;

                //to check if there is Line Of Sight towards Player
                if (_hit1.collider != null)
                {
/*                    Debug.Log(hit.collider);
*/                    EnemyState = State.ChaseTarget;
                }

                //to delay each fire a bit
                if (Time.time >= _nextAvailFire)
                {
/*                    if (Vector2.Distance(transform.position, Player.transform.position) < 3)
                    {
                        state = State.PlayerMoveAway;
                    }*/
                    Fire();
                    _nextAvailFire = Time.time + 1 / _fireRate;
                }
                Debug.DrawLine(transform.position, _initialArm, Color.red);
                Debug.DrawLine(transform.position, _nextArm, Color.red);

                //to move away if player is too close

                //to move towards if player is too far
                if (Vector2.Distance(transform.position, Player.transform.position) > 6)
                {
                    EnemyState = State.ChaseTarget;
                }
                break;

            case State.PlayerMoveAway:
                _path.canMove = false;

                //move opposite direction from direction of player
                Vector2 dirPlayer = -(Player.transform.position - transform.position);
                _rigidbody.MovePosition(_rigidbody.position + dirPlayer * Time.fixedDeltaTime);

                //if player distance is too far, chase target
                if (Vector2.Distance(transform.position, Player.transform.position) > 5f)
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
        if(EnemyState == State.Shooting){
            _animator.SetBool("IsShooting", true);
        }else{
            _animator.SetBool("IsShooting", false);
        }

    }

    private void Fire() {
        Vector3 shootDir = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        GameObject clone = Instantiate(EnemyShot, transform.position + shootDir.normalized, Quaternion.Euler(0f,0f, angle));
        _shotBody = clone.GetComponent<Rigidbody2D>();
/*        float playerAngle = Vector2.Angle(initialArm, nextArm) * Mathf.Deg2Rad;
        Vector2 vec = new Vector2(Mathf.Cos(playerAngle), Mathf.Sin(playerAngle));*/        
        _shotBody.AddForce(shootDir.normalized * _projectileSpeed, ForceMode2D.Impulse);
        clone.GetComponent<EnemyShot>().SetDamage(_damage);
    }

    private void FindTargetRanger()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < 6)
        {
            EnemyState = State.Shooting;
        }
    }



}
