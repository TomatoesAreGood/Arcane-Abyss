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
        isOpen = true;
        PauseManager.instance.Pause();
        gameObject.SetActive(true);
        UpdateData();
    }

    public void Disable(){
        isOpen = false;
        PauseManager.instance.Resume();
        gameObject.SetActive(false);
        UpdateData();
    }
    
    public void UpdateData(){
        inventoryRenderer.UpdateData();
        potionBagRenderer.UpdateData();
        spellsRenderer.UpdateData();
    }

    public void MergeSortSortAlpha(){
        inventoryRenderer.MergeSortSortAlpha();
    }
    public void BubbleSortValue(){
        inventoryRenderer.BubbleSortValue();
    }

    public void PigeonHoleSortOcurrances(){
        inventoryRenderer.PigeonHoleSortOcurrances();
    }

   
}
