using UnityEngine;
using System;
using UnityEngine.UI;


public class ReferencedItem : Item{
    public GameObject reference;
    private Image spriteImage;
    protected virtual void Start(){
        if (reference == null){
            throw new ArgumentException("no object reference attached to item");
        }   
        spriteImage = gameObject.GetComponent<Image>();
        spriteImage.sprite = reference.GetComponent<SpriteRenderer>().sprite;
    }
}