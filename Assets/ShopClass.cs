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
        Item[] itemlibrary = ItemLibrary.instance.Library;
        Item[] shopslots = new Item[4];
        for (int i = 0; i < 4; i++)
        {
            if (itemlibrary[i] != null)
            {
                int rnd = Random.Range(0, itemlibrary.Length);
                shopslots[i] = itemlibrary[rnd];
            }
                
        }
        for(int i = 0;i < Slots.Length; i++)
        {
            Instantiate(shopslots[i].gameObject, Slots[i].transform,false);
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
