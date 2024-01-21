using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public void NewGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DataPersistanceManager.instance.NewGame();

    }
    public void Load()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        DataPersistanceManager.instance.LoadGame();

    }
    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ClsoeOptionsMenu()
    {
        optionsMenu.SetActive(false);
        gameObject.SetActive(true);

    }
    public void Exit()
    {
        Application.Quit();
        Debug.Break();
    }
    //Code from Brackey's "START MENU in Unity" from Youtube
}
