using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventoryRenderer inventoryRenderer;
    public InventoryRenderer potionBagRenderer;
    public InventoryRenderer spellsRenderer;
    public bool isOpen;


    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    private void Update() {    
        if (isOpen){
            gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
        }

    }

    public void Enable(){
        Time.timeScale = 0f;
        isOpen = true;
        gameObject.SetActive(true);
    }
    public void Disable(){
        Time.timeScale = 1f;
        isOpen = false;
        gameObject.SetActive(false);
        inventoryRenderer.UpdateData();
        potionBagRenderer.UpdateData();
        spellsRenderer.UpdateData();
    }
}
