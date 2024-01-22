using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

using System;

public class SpawnManager : MonoBehaviour
{

    public Tilemap Tilemap;
    public GameObject TestSpawn;
    private List<Vector3> _availablePlaces;
    private float _spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
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

                if (!Physics2D.OverlapBox(place, new Vector2(1, 1), 0))
                {
                    _availablePlaces.Add(place);

                }
            }
        }
    }

    public void SpawnEnemy()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer > 1)
        {
            Vector3 randomSpawn = _availablePlaces[UnityEngine.Random.Range(0, _availablePlaces.Count)];
            Instantiate(TestSpawn, randomSpawn, Quaternion.Euler(0, 0, 0));
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
