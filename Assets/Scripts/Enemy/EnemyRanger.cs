using Pathfinding;
using System.Collections;
using System.Collections.Generic;
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
    private Vector2 initialArm;
    private Vector2 nextArm;
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
    }

    // Update is called once per frame

    protected override void FixedUpdate()
    {
        switch (state)
        {
            default:
            case State.ChaseTarget:
                _path.canMove = true;
                FindTargetRanger();
                FindEnemy();
                break;

            case State.MoveAway:
                _path.canMove = false;
                transform.position = Vector2.MoveTowards(transform.position.normalized, hit.point.normalized, -3f * Time.deltaTime);
                if ((Vector2.Distance(transform.position, hit.point) > 2.5f))
                {
                    state = State.ChaseTarget;
                }
                break;

            case State.Shooting:
                _path.canMove = false;
                if (Time.time >= nextAvailFire)
                {
                    Fire();
                    nextAvailFire = Time.time + 1 / fireRate;
                }
                Debug.DrawLine(transform.position, initialArm, Color.red);
                Debug.DrawLine(transform.position, nextArm, Color.red);
                if (Vector2.Distance(transform.position, Player.transform.position) < 3)
                {
                    state = State.PlayerMoveAway;
                }

                if (Vector2.Distance(transform.position, Player.transform.position) > 6)
                {
                    state = State.ChaseTarget;
                }
                break;
            case State.PlayerMoveAway:
                _path.canMove = false;
                Vector2 dir = -(Player.transform.position - transform.position);
                _rigidbody.MovePosition(_rigidbody.position + dir * Time.fixedDeltaTime);
                if ((Vector2.Distance(transform.position, Player.transform.position) > 5f))
                {
                    state = State.ChaseTarget;
                }
                break;




        }
    }

    private void Fire() {
        Vector3 shotDir = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(shotDir.y, shotDir.x) * Mathf.Rad2Deg;
        GameObject clone = Instantiate(EnemyShot, transform.position + shotDir.normalized, Quaternion.Euler(0f,0f, angle));
        shotBody = clone.GetComponent<Rigidbody2D>();
/*        float playerAngle = Vector2.Angle(initialArm, nextArm) * Mathf.Deg2Rad;
        Vector2 vec = new Vector2(Mathf.Cos(playerAngle), Mathf.Sin(playerAngle));*/
        shotBody.AddForce(Player.transform.position - transform.position, ForceMode2D.Impulse);
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
