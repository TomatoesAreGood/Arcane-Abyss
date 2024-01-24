using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class DeathManager : MonoBehaviour, IDataPersistance
{
    public DeathMenu deathMenu;
    public static DeathManager instance;
    public bool isDead;
    public float timeAlive;

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
        deathMenu.Disable();
        isDead = false;
    }
   
    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (isDead)
        {
            deathMenu.Enable();
            Time.timeScale = 0f;
        }

    }

    public void LoadData(GameData data)
    {
        timeAlive = data.timeAlive;
    }

    public void SaveData(ref GameData data)
    {
        data.timeAlive = timeAlive;
    }
}
