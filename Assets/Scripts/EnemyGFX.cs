using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGFX : MonoBehaviour
{

    private Vector2 _direction;
    public SpriteRenderer SpriteRenderer;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _direction = Player.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        if (_direction.x < 0)
        {
            SpriteRenderer.flipX = true;
        }
        else
        {
            SpriteRenderer.flipX = false;
        }
    }
}
