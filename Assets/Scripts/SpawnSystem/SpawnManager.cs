using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

using System;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject EnemyPouncer;
    public GameObject EnemyRanger;
    public GameObject EnemySpellCaster;
    public GameObject EnemyTank;
    private GameObject[] _enemyPool;
    public Tilemap Tilemap;
    public GameObject TestSpawn;
    private List<Vector3> _availablePlaces;
    private float _spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        _enemyPool = new GameObject[]{Enemy, EnemyPouncer, EnemyRanger, EnemySpellCaster , EnemyTank};

        FindAvailablePlaces();
        Debug.Log("start test");
    }


    public void FindAvailablePlaces()
    {
        _availablePlaces = new List<Vector3>();
        for (int x = Tilemap.cellBounds.xMin;  x <= Tilemap.cellBounds.xMax; x++)
        {
/*            Debug.Log($"x :{x}");
*/            for (int y = Tilemap.cellBounds.yMin; y <= Tilemap.cellBounds.yMax; y++)
            {
/*                Debug.Log($"y :{y}");
*/
                Vector3Int localPlace  = new Vector3Int(x, y, 0);


                Vector3 place = Tilemap.CellToWorld(localPlace);

                var sprite = Tilemap.GetSprite(localPlace);
                var tile = Tilemap.GetTile(localPlace);
                if (tile != null && sprite != null) {
                    if (!Physics2D.OverlapBox(place, new Vector2(1, 1), 0))
                    {
                        _availablePlaces.Add(place);

                    }
                }

            }
        }
    }

    public void SpawnEnemy()
    {

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer > 1)
        {
            GameObject randomEnemy = _enemyPool[UnityEngine.Random.Range(0, _enemyPool.Length - 1)];
            Vector3 randomSpawn = _availablePlaces[UnityEngine.Random.Range(0, _availablePlaces.Count)];
            Instantiate(randomEnemy, randomSpawn, Quaternion.Euler(0, 0, 0));
            Debug.Log("instantiated");
            _spawnTimer = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }
}
