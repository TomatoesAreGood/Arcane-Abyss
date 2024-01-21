using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public int[] itemsIDs;
    public List<string> enemies;
    
    public GameData(){
        enemies = new List<string>();
        itemsIDs = new int[40];
    }
}
