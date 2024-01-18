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
    public GameObject[] Slots; 




    void Start()
    {
        ShopUi.SetActive(false);
        _isPaused = false;
        object[] itemlibrary = ItemLibrary.instance.itemsArray;
        GameObject[] shopslots = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            if (itemlibrary[i] != null)
            {
                int rnd = Random.Range(0, itemlibrary.Length);
                shopslots[i] = itemlibrary[rnd] as GameObject;
            }
                
        }
        for(int i = 0;i < Slots.Length; i++)
        {
            Instantiate(shopslots[i], Slots[i].transform,false);
        }
        







    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            
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
