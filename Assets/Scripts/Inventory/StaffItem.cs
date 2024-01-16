using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffItem : ReferencedItem
{
    protected override void Start(){
        base.Start();
        if (reference.GetComponent<Staff>() == null){
            throw new ArgumentException("object reference is not a staff");
        }
    }
  
}
