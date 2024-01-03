using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{

    public Slider Slider;

    public void SetMaxMana(int maxMana)
    {
        Slider.maxValue = maxMana;
    }
    public void SetMana(int mana)
    {
        Slider.value = mana;
    }
    //Code from Brackey's "How to make a HEALTH BAR in Unity!" from Youtube
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
