using UnityEngine;
using System;

public abstract class SpellItem : ReferencedItem{

    protected override void Start(){
        base.Start();
        if (reference.GetComponent<Spell>() == null){
            throw new ArgumentException("object reference is not a spell");
        }
    }

}