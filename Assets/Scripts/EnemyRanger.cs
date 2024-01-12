using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanger : Enemy
{
    public GameObject EnemyShot;
    private Rigidbody2D shotBody;
    // Start is called before the first frame update
    void Start()
    {
        shotBody = EnemyShot.GetComponent<Rigidbody2D>();
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
                StartCoroutine(MoveAwayHandler());
                if ((Vector2.Distance(transform.position, hit.point) > 2.5f))
                {
                    state = State.ChaseTarget;
                }
                break;
            case State.Shooting:
                _path.canMove = false;
                StartCoroutine(ShotHandler());
                Instantiate(EnemyShot, transform.position, Quaternion.identity);
                shotBody.AddForce(Player.transform.position, ForceMode2D.Force);
                break;
        }
    }

    private void FindTargetRanger()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < 5)
        {
            //player is within pounching range
            Debug.Log("within range");
            state = State.Shooting;
        }
    }

    IEnumerator ShotHandler()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
