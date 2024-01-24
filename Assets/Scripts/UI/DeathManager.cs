using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class DeathManager : MonoBehaviour
{
    public DeathMenu deathMenu;
    public static DeathManager instance;
    public bool isDead;

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
        if (isDead)
        {
            deathMenu.Enable();
            Time.timeScale = 0f;
        }
    }

}
