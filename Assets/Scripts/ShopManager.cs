using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public bool IsPaused;
    public TextMeshProUGUI Balance; 
    public ShopUI ShopUI;
   
  

    private void Start()
    {
        //Creates an instance that allows other scripts to refrence this class
        if (instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
        ShopUI.Disable();
        IsPaused = false;
    }
    // Update is called once per frame
    private void Update()
    {
        Balance.text = "" + PlayerController.instance.coins;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (IsPaused)
            {
                IsPaused = false;
                ShopUI.Disable();
                PauseManager.instance.Resume();
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerIsTrigger"))
        {
                IsPaused = true;
                ShopUI.Enable();
                PauseManager.instance.Pause();
        }
    }
}
