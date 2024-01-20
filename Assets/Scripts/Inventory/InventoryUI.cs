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
    public bool isOpen;
    [SerializeField] Image equippedStaff;


    // Start is called before the first frame update
    private void Awake()
    {
        isOpen = false;
        
        inventoryRenderer.width = PlayerController.instance.inventoryWidth;
        inventoryRenderer.height = PlayerController.instance.inventoryHeight;

        potionBagRenderer.width = 1;
        potionBagRenderer.height = PlayerController.instance.potionBagSize;

        spellsRenderer.width = PlayerController.instance.spellInventorySize;
        spellsRenderer.height = 1;

        equippedSpellsRenderer.width = 4;
        equippedSpellsRenderer.height = 1;
    }

    private void Update() {    
        if (isOpen){
            if(PlayerController.instance.inventory.equippedStaff != null){
                equippedStaff.gameObject.SetActive(true);
                equippedStaff.sprite = PlayerController.instance.equippedStaff.GetComponent<SpriteRenderer>().sprite;           
            }else{
                equippedStaff.gameObject.SetActive(false);
            }
            UpdateData();
            
            gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
        }
    }

    public void Enable(){
        Time.timeScale = 0f;
        isOpen = true;
        gameObject.SetActive(true);
        UpdateData();
    }

    public void Disable(){
        Time.timeScale = 1f;
        isOpen = false;
        gameObject.SetActive(false);
        UpdateData();
    }
    
    public void UpdateData(){
        inventoryRenderer.UpdateData();
        potionBagRenderer.UpdateData();
        spellsRenderer.UpdateData();
    }
}
