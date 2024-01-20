using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyRanger : Enemy
{
    public GameObject EnemyShot;
    private Rigidbody2D shotBody;
    private bool canShoot = true;
    private bool isShotHandlerRunning = false;
    protected float nextAvailFire;
    protected float fireRate;
    protected int damage;
    protected float projectileSpeed;
    private Vector2 initialArm;
    private Vector2 nextArm;
    protected Animator animator;
    protected RaycastHit2D _hit1;
    protected RaycastHit2D _hit2;
    protected RaycastHit2D hit3;

    // Start is called before the first frame update
    protected override void Start()
    {
        Health = 5;
        Player = PlayerController.instance.gameObject;
        state = State.ChaseTarget;
        nextAvailFire = Time.time;
        _moveSpeed = 2.5f;
        damage = 1;
        fireRate = 0.3f;
        projectileSpeed = 6f;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame

    protected override void FixedUpdate()
    {
        Vector2 losDir = Player.transform.position - transform.position;
        var dis = Vector2.Distance(Player.transform.position, transform.position);
        _hit1 = Physics2D.CircleCast(AsVector2(transform.position) + losDir.normalized, 1,losDir, dis);
       /* hit2 = Physics2D.Raycast(AsVector2(transform.position) + Vector2.Perpendicular(transform.position).normalized + losDir.normalized, losDir + Vector2.Perpendicular(transform.position).normalized, dis);
        hit3 = Physics2D.Raycast(AsVector2(transform.position) + -Vector2.Perpendicular(transform.position).normalized + losDir.normalized, losDir + Vector2.Perpendicular(transform.position).normalized, dis);*/
        /*Debug.DrawLine(transform.position, losDir);
        Debug.DrawLine(AsVector2(transform.position) + Vector2.Perpendicular(transform.position).normalized, losDir + Vector2.Perpendicular(transform.position).normalized);
        Debug.DrawLine(AsVector2(transform.position) + -Vector2.Perpendicular(transform.position).normalized + losDir.normalized, losDir + -Vector2.Perpendicular(transform.position).normalized);*/

        Debug.Log(state);

        switch (state)
        {
            default:
            case State.ChaseTarget:
                _path.canMove = true;

                //if there is no obstacle blocking Line Of Sight of circle cast, find target
                if (!_hit1.collider.CompareTag("Obstacle") && _hit1.collider.gameObject != gameObject)
                {
                    Debug.Log(_hit1.collider);
                    FindTargetRanger();
                }
               
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

            case State.Shooting:
                _path.canMove = false;

                //to check if there is Line Of Sight towards Player
                if (_hit1.collider.CompareTag("Obstacle") && _hit1.collider.gameObject != gameObject)
                {
                    Debug.Log(hit.collider);
                    state = State.ChaseTarget;
                }

                //to delay each fire a bit
                if (Time.time >= nextAvailFire)
                {
                    Fire();
                    nextAvailFire = Time.time + 1 / fireRate;
                }
                Debug.DrawLine(transform.position, initialArm, Color.red);
                Debug.DrawLine(transform.position, nextArm, Color.red);

                //to move away if player is too close
                if (Vector2.Distance(transform.position, Player.transform.position) < 3)
                {
                    state = State.PlayerMoveAway;
                }
                //to move towards if player is too far
                if (Vector2.Distance(transform.position, Player.transform.position) > 6)
                {
                    state = State.ChaseTarget;
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
                    state = State.ChaseTarget;
                }
                break;
        }
        if(state == State.Shooting){
            animator.SetBool("IsShooting", true);
        }else{
            animator.SetBool("IsShooting", false);
        }

    }

    private void Fire() {
        Vector3 shootDir = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        GameObject clone = Instantiate(EnemyShot, transform.position + shootDir.normalized, Quaternion.Euler(0f,0f, angle));
        shotBody = clone.GetComponent<Rigidbody2D>();
/*        float playerAngle = Vector2.Angle(initialArm, nextArm) * Mathf.Deg2Rad;
        Vector2 vec = new Vector2(Mathf.Cos(playerAngle), Mathf.Sin(playerAngle));*/        
        shotBody.AddForce(shootDir.normalized * projectileSpeed, ForceMode2D.Impulse);
        clone.GetComponent<EnemyShot>().SetDamage(damage);
    }

    private void FindTargetRanger()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < 6)
        {
            state = State.Shooting;
        }
    }



}
