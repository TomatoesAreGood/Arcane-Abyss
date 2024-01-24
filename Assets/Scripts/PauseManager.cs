using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    private void Awake()
    {
        Time.timeScale = 1f;
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public void Pause(){
        Time.timeScale = 0f;
    }

    public void Resume(){
        if(PlayerController.instance.inventoryUI.isOpen || PauseMenu.instance.isPaused || ShopManager.instance.IsPaused){
            return;
        }
        Time.timeScale = 1f;
    }
    
}
