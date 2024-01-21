using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public Item[] items;
    public List<string> enemies;
    
    public GameData(){
        enemies = new List<string>();
        items = new Item[40];
    }
}
