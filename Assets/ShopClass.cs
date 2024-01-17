using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopClass : MonoBehaviour
{
    // Start is called before the first frame update

    


        
    void Start()
    {
        ItemLibrary itemsLibraryscirpt = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemLibrary>();
        object[] itemlibrary = ItemLibrary.instance.itemsArray;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
