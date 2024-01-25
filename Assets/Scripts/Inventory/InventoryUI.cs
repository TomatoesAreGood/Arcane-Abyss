using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] TextMeshProUGUI damageBonus;

    // Start is called before the first frame update
    private void Awake()
    {
        isOpen = false;

        inventoryRenderer.width = PlayerController.Instance.inventoryWidth;
        inventoryRenderer.height = PlayerController.Instance.inventoryHeight;

        potionBagRenderer.width = 1;
        potionBagRenderer.height = PlayerController.Instance.potionBagSize;

        spellsRenderer.width = PlayerController.Instance.spellInventorySize;
        spellsRenderer.height = 1;

        equippedSpellsRenderer.width = 4;
        equippedSpellsRenderer.height = 1;
    }

    private void Update() {    
        if (isOpen){
            if(PlayerController.Instance.inventory.equippedStaff != null){
                equippedStaff.gameObject.SetActive(true);
                equippedStaff.sprite = PlayerController.Instance.equippedStaff.GetComponent<SpriteRenderer>().sprite;        
                damageBonus.text = "+" + PlayerController.Instance.inventory.equippedStaff.damageBonus + " Damage Bonus";   
            }else{
                equippedStaff.gameObject.SetActive(false);
                damageBonus.text = "+0 Damage Bonus";
            }
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
