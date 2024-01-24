using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    public static DeathMenu instance;
    public bool isDead;
    public TextMeshProUGUI _enemiesText;

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
        deathMenu.SetActive(false);
        isDead = false;
    }
   
    private void Update()
    {
        if (isDead)
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnEnable() 
    {
        FinalStats.instance.PigeonHoleSort();
        PrintKeysAndValues();
    }

    private void PrintKeysAndValues()
    {
        string enemiesTxt = "";
        foreach (KeyValuePair<string, int> kvp in FinalStats.instance.pigeonHoleSortedEnemies)
        {
            enemiesTxt += kvp.Key + ": " + kvp.Value;
        }
        _enemiesText.text = enemiesTxt;
    }
}
