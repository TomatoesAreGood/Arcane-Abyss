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

    // Freezes the time
    public void Pause(){
        Time.timeScale = 0f;
    }

    public void Resume(){
        if(PlayerController.Instance.inventoryUI.IsOpen || PauseMenu.instance.isPaused || ShopManager.Instance.GetPauseStatus()){
            return;
        }
        Time.timeScale = 1f;
    }
    
}
