using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public int[] itemsIDs;
    public int[] potionIDs;
    public List<string> killedEnemies;
    public Vector3 playerPos;
    public List<Vector3> enemyPositions;
    public List<int> enemyTypes;
    public int health;
    public float mana;

    public GameData(){
        killedEnemies = new List<string>();
        itemsIDs = new int[40];
        potionIDs = new int[4];
        playerPos = Vector3.zero;
        enemyPositions = new List<Vector3>();
        enemyTypes = new List<int>();
        health = 5;
        mana = 100;
    }
}
