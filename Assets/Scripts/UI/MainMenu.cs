using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public void NewGame()
    {
        File.Delete(DataPersistanceManager.instance.dataHandler.fullPath);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void Load()
    {
        DataPersistanceManager.instance.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

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
