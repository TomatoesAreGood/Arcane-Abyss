using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public static PauseMenu instance;
    public bool isPaused;

    private void Start()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // When the escape key is pressed, the pause menu activates
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        PauseManager.instance.Pause();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        PauseManager.instance.Resume();
    }

    public void ReturntoMain()
    {
        DataPersistanceManager.Instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}