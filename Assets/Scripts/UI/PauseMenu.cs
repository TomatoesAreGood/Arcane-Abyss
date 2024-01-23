using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static PauseMenu instance;
    public bool isPaused;

    // Start is called before the first frame update
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

    // Update is called once per frame
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
        DataPersistanceManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}