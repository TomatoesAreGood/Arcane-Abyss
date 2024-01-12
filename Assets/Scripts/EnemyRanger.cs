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
    private float nextAvailFire;
    private float fireRate;

    private Vector2 initialArm;
    private Vector2 nextArm;
    // Start is called before the first frame update
    void Start()
    {
        state = State.ChaseTarget;
        _path = GetComponent<AIPath>();
        nextAvailFire = Time.time;
        fireRate = 0.5f;


    }

    // Update is called once per frame
    void Update()
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
                if (Time.time >= nextAvailFire) {
                    Debug.Log("shooting");
                    Fire();
                    nextAvailFire = Time.time + 1/fireRate;
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
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -3f * Time.deltaTime);
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
        Debug.Log(angle);
        GameObject clone = Instantiate(EnemyShot, transform.position + shotDir.normalized, Quaternion.Euler(0f,0f, angle));
        shotBody = clone.GetComponent<Rigidbody2D>();


        
/*        float playerAngle = Vector2.Angle(initialArm, nextArm) * Mathf.Deg2Rad;
        Vector2 vec = new Vector2(Mathf.Cos(playerAngle), Mathf.Sin(playerAngle));*/
        shotBody.AddForce(Player.transform.position - transform.position, ForceMode2D.Impulse);
    }

    private void FindTargetRanger()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < 6)
        {
            //player is within pounching range
            Debug.Log("within range");
            state = State.Shooting;
        }
    }



}
