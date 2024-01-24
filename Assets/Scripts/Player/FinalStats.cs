using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStats : MonoBehaviour, IDataPersistance
{
    public static FinalStats instance;
    
    public static List<string> enemies;
    public Dictionary<string, int> pigeonHoleSortedEnemies;

    /*
     * - Make an array of strings - for each monster killed, add the name/label of that monster into the array. At the end, loop through and create 
     *  a dictionary (just like socks code) and have keys and values.
    */

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        enemies = new List<string>();
        pigeonHoleSortedEnemies = new Dictionary<string, int>();
    }



    //void Start()
    //{
        
    //}

    public void PigeonHoleSort()
    {
        string a = "";
        foreach (string enemy in enemies)
        {
            a += enemy + ", ";
        }
        Debug.Log(a);

        foreach (string enemy in enemies)
        {
            if (pigeonHoleSortedEnemies.ContainsKey(enemy)){
                pigeonHoleSortedEnemies[enemy] += 1;
            } else {
                pigeonHoleSortedEnemies[enemy] = 1;
            }
        }
    }

    public void LoadData(GameData data){
        enemies = data.killedEnemies;
    }

    public void SaveData(ref GameData data){
        data.killedEnemies = enemies;
    }

}
