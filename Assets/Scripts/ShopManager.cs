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
    public static ShopManager Instance;
    private bool _isPaused;
    public TextMeshProUGUI Balance;
    public ShopUI ShopUI;



    private void Start()
    {
        //Creates an instance that allows other scripts to refrence this class
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        ShopUI.Disable();
        _isPaused = false;
    }
    // Update is called once per frame
    private void Update()
    {
        Balance.text = "" + PlayerController.Instance.Coins;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_isPaused)
            {
                _isPaused = false;
                ShopUI.Disable();
                PauseManager.instance.Resume();
            }

        }




    }

    public bool GetPauseStatus()
    {
        return _isPaused;
    }

    public void SetPauseStatus(bool value)
    {
        this._isPaused = value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerIsTrigger"))
        {

            _isPaused = true;
            ShopUI.Enable();
            PauseManager.instance.Pause();


        }
    }
}