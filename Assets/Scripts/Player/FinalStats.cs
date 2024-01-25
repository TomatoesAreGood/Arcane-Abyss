using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStats : MonoBehaviour, IDataPersistance
{
    public static FinalStats instance;
    public List<string> killedEnemies;
    public Dictionary<string, int> pigeonHoleSortedEnemies;

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
        
        // List to store the name of each enemy killed
        killedEnemies = new List<string>();

        // Dictionary to sort through the list above when the death screen is activated - creates a key for each type of enemy and a value for the number of respective enemies killed
        pigeonHoleSortedEnemies = new Dictionary<string, int>();
    }

    // Method that adds the name of the enemy object killed to the killedEnemies list
    public void AddEnemy(Enemy enemy)
    {
        killedEnemies.Add(enemy.GetType().Name);
    }

    public void PigeonHoleSort()
    {
        foreach (string enemy in killedEnemies)
        {
            if (pigeonHoleSortedEnemies.ContainsKey(enemy)){
                pigeonHoleSortedEnemies[enemy] += 1;
            } else {
                pigeonHoleSortedEnemies[enemy] = 1;
            }
        }
    }

    public void LoadData(GameData data){
        killedEnemies = data.killedEnemies;
    }

    public void SaveData(ref GameData data){
        data.killedEnemies = killedEnemies;
    }

}
