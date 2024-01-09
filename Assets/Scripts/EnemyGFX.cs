using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGFX : MonoBehaviour
{

    private Vector2 _direction;
    private SpriteRenderer _spriteRenderer;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _direction = Player.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}
