using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

using System;
using System.ComponentModel.Design.Serialization;
using System.Data;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject EnemyPouncer;
    public GameObject EnemyRanger;
    public GameObject EnemySpellCaster;
    public GameObject EnemyTank;
    private GameObject[] _enemyPool;
    public GameObject Chest;
    public GameObject StaffChest;
    public GameObject SpellBookChest;
    public GameObject PotionChest;
    private GameObject[] _chestPool;

    public Tilemap Tilemap;
    public GameObject TestSpawn;
    private List<Vector3> _availablePlaces;
    private float _spawnTimer;
    private float _waveTimer;
    private float _spawnRate;
    private float _waveNum;
    private float _waveTime;
    private float _chestTimer;
    private bool _isStart = true;
    // Start is called before the first frame update
    void Start()
    {
        _enemyPool = new GameObject[]{Enemy, EnemyPouncer, EnemyRanger, EnemySpellCaster , EnemyTank};
        _chestPool = new GameObject[] {Chest, PotionChest, SpellBookChest, StaffChest};
        _spawnRate = 10;
        _waveNum = 0;
        FindAvailablePlaces();
        Debug.Log("start test");
        _waveTime = 7;
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
    public void SetWaveNum(float waveNum)
    {
        if (waveNum < 0)
        {
            throw new ArgumentException("ERROR : _waveNum cannot be negative");
        }
        _waveNum = waveNum;
    }

    public void SetSpawnRate(float spawnRate)
    {
        if (spawnRate < 2)
        {
            throw new ArgumentException("ERROR: _spawnRate cannot be below 2");
        }
        _spawnRate = spawnRate;
    }

    public float GetWaveNum()
    {
        return _waveNum;
    }


    public float GetSpawnRate()
    {
        return _spawnRate;
    }



    public void WaveManager()
    {
        _waveTimer += Time.deltaTime;

        if (_waveTimer > _waveTime) {
            if (_spawnRate > 2) {
                _spawnRate -= 1;
            }
            Debug.Log(_waveNum);
            _waveNum += 1;
            _waveTime = 60;
            _waveTimer = 0;
        }
    }

    public void SpawnEnemy()
    {

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer > _spawnRate)
        {
            GameObject randomEnemy = _enemyPool[UnityEngine.Random.Range(0, _enemyPool.Length)];
            Vector3 randomSpawn = _availablePlaces[UnityEngine.Random.Range(0, _availablePlaces.Count)];
            Instantiate(randomEnemy, randomSpawn, Quaternion.Euler(0, 0, 0));
            _spawnTimer = 0;
        }
        
    }

    public void SpawnChest()
    {
        _chestTimer += Time.deltaTime;
        if (_chestTimer > 7)
        {
            GameObject randomChest = _chestPool[UnityEngine.Random.Range(0, _chestPool.Length)];
            Vector3 randomSpawn = _availablePlaces[UnityEngine.Random.Range(0, _availablePlaces.Count)];
            Instantiate(randomChest, randomSpawn, Quaternion.Euler(0, 0, 0));
            _chestTimer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        WaveManager();
        SpawnEnemy();
        SpawnChest();
    }
}
