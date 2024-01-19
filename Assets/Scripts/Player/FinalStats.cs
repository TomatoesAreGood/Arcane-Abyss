using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStats : MonoBehaviour
{
    public List<string> enemies;
    public Dictionary<string, int> pigeonHoleSortedEnemies;

    /*
     * magic spells shot
     * kills
     * net worth
     * 
     * 
     * - Make an array of strings - for each monster killed, add the name/label of that monster into the array. At the end, loop through and create 
     *  a dictionary (just like socks code) and have keys and values.
    */
    
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<string>();
        pigeonHoleSortedEnemies = new Dictionary<string, int>();
    }

    public void PigeonHoleSort()
    {
        foreach (string enemy in enemies)
        {
            if (pigeonHoleSortedEnemies.ContainsKey(enemy))
                pigeonHoleSortedEnemies[enemy]++;
            else
                pigeonHoleSortedEnemies[enemy] = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
