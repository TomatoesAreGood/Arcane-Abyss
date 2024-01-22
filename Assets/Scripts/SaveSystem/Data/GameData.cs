using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public int[] itemsIDs;
    public List<string> enemies;
    public Vector3 playerPos;
    public List<Vector3> enemyPositions;
    public List<int> enemyTypes;

    public GameData(){
        enemies = new List<string>();
        itemsIDs = new int[40];
        playerPos = Vector3.zero;
        enemyPositions = new List<Vector3>();
        enemyTypes = new List<int>();
    }
}
