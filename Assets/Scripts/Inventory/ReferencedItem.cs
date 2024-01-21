using UnityEngine;
using System;
using UnityEngine.UI;


public class ReferencedItem : Item{
    public GameObject reference;
    private Image spriteImage;
    protected override void Start(){
        base.Start();
        if (reference == null){
            throw new ArgumentException("no object reference attached to item");
        }   
        spriteImage = gameObject.GetComponent<Image>();
        spriteImage.sprite = reference.GetComponent<Spell>().spellShot.GetComponent<SpriteRenderer>().sprite;
    }
}