using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : ReferencedItem
{
    public override void Use(){
        throw new ArgumentException("not implemented");
    }
}
