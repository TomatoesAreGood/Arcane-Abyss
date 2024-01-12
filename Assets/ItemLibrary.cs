using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{
    public static ItemLibrary instance;
    public GameObject basicStaff;
    // Start is called before the first frame update
    private void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
