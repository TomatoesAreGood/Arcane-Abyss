using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventoryRenderer inventoryRenderer;
    public InventoryRenderer potionBagRenderer;
    public InventoryRenderer spellsRenderer;
    public InventoryRenderer equippedSpellsRenderer;
    public Transform PlayerUICanvas;
    public bool isOpen;
    [SerializeField] Image equippedStaff;


    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

    }

    private void Update() {    
        if (isOpen){
            equippedStaff.sprite = PlayerController.instance.equippedStaff.GetComponent<SpriteRenderer>().sprite;
            for(int i = 0; i < 4; i++){
                equippedSpellsRenderer.GetSlot(i).item = PlayerController.instance.inventory.equippedSpells[i];
            }
            //if performance issues, move this to when spells are equipped
            equippedSpellsRenderer.RedrawMatrix();
           
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
