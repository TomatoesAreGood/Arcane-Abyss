using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    public static DeathMenu instance;
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
}
