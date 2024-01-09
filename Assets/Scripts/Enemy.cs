using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int _damage;
    protected int _speed;
    public GameObject Player;
    protected PlayerController _playerScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
