using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : Item
{
    public override void Use(){
        throw new ArgumentException("not implemented");
    }
}
