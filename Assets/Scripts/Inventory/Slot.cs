using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour
{
    public Item item;

    public bool IsEmpty(){
        //return item == null;
        return transform.childCount == 0;
    }
  
    public void Dim(){

        

    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
