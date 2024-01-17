using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class ShopClass : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ShopUi;
    private bool _isPaused; 




    void Start()
    {
        ShopUi.SetActive(false);
        _isPaused = false;
        ItemLibrary itemsLibraryscirpt = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemLibrary>();
        object[] itemlibrary = ItemLibrary.instance.itemsArray;
        object[] shopslots = new object[8];
        for (int i = 0; i < 4; i++)
        {
            int rnd = Random.Range(0, itemlibrary.Length);
            shopslots[i] = itemlibrary[rnd];
        }
        







    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("pressed Q");
            
            if (_isPaused)
            {
                

                ShopUi.SetActive(false);
                Time.timeScale = 1f;
                _isPaused = false;
            }
            else
            {
                ShopUi.SetActive(true);
                Time.timeScale = 0f;
                _isPaused = true;
            }


            
          




        }


    }
}
