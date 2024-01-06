using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FindTarget()
    {
        float targetRange = 5f;
        if (Vector2.Distance(transform.position, Player.transform.position) < targetRange) {
            //player is within pounching range
            Debug.Log("within range");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
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
