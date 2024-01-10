using UnityEngine;
using System;

public class ReferencedItem : Item{
    public GameObject reference;
    protected virtual void Start(){
        if (reference == null){
            throw new ArgumentException("no object reference attached to item");
        }   
    }
}