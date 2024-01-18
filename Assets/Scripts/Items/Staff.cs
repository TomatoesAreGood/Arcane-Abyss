using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Staff : MonoBehaviour
{
    public int damageBonus;
    protected Transform aimTransform; //pivot
   
    protected virtual void Start(){
        aimTransform = transform.parent;
        damageBonus = 0;
    }
    


}
