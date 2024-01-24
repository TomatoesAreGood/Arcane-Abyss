using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public int[] itemsIDs;
    public int[] potionIDs;
    public int[] spellsIDs;

    public List<string> killedEnemies;
    public Vector3 playerPos;
    public List<Vector3> enemyPositions;
    public List<int> enemyTypes;
    public int health;
    public float mana;
    public float musicVolume;
    public int maxHealth;
    public int coins;

    public GameData(){
        killedEnemies = new List<string>();
        itemsIDs = new int[40];
        potionIDs = new int[4];
        spellsIDs = new int[8];
        playerPos = Vector3.zero;
        enemyPositions = new List<Vector3>();
        enemyTypes = new List<int>();
        health = 5;
        maxHealth = 5;
        mana = 100;
        musicVolume = 0.75f;
        itemsIDs[0] = 5;
        itemsIDs[1] = 20;

        spellsIDs[0] = 3;
        potionIDs[0] = 10;
        coins = 0;
    }
}
