using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventoryRenderer InventoryRenderer;
    public InventoryRenderer PotionBagRenderer;
    public InventoryRenderer SpellsRenderer;
    public InventoryRenderer EquippedSpellsRenderer;
    public bool IsOpen;
    [SerializeField] private Image _equippedStaff;
    [SerializeField] private TextMeshProUGUI _damageBonus;

    // Start is called before the first frame update
    private void Awake()
    {
        IsOpen = false;

        //declare sizes of inventory renderers based on variables set in PlayerController
        InventoryRenderer.Width = PlayerController.Instance.inventoryWidth;
        InventoryRenderer.Height = PlayerController.Instance.inventoryHeight;

        PotionBagRenderer.Width = 1;
        PotionBagRenderer.Height = PlayerController.Instance.potionBagSize;

        SpellsRenderer.Width = PlayerController.Instance.spellInventorySize;
        SpellsRenderer.Height = 1;

        EquippedSpellsRenderer.Width = 4;
        EquippedSpellsRenderer.Height = 1;
    }

    private void Update() {    
        if (IsOpen){
            if(PlayerController.Instance.inventory.EquippedStaff != null){
                _equippedStaff.gameObject.SetActive(true);
                _equippedStaff.sprite = PlayerController.Instance.equippedStaff.GetComponent<SpriteRenderer>().sprite;        
                _damageBonus.text = "+" + PlayerController.Instance.inventory.EquippedStaff.damageBonus + " Damage Bonus";   
            }else{
                _equippedStaff.gameObject.SetActive(false);
                _damageBonus.text = "+0 Damage Bonus";
            }
            gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
        }
    }

    public void Enable(){
        IsOpen = true;
        PauseManager.instance.Pause();
        gameObject.SetActive(true);
        UpdateData();
    }

    public void Disable(){
        IsOpen = false;
        PauseManager.instance.Resume();
        gameObject.SetActive(false);
        UpdateData();
    }
    
    public void UpdateData(){
        InventoryRenderer.UpdateData();
        PotionBagRenderer.UpdateData();
        SpellsRenderer.UpdateData();
    }

    public void MergeSortSortAlpha(){
        InventoryRenderer.MergeSortSortAlpha();
    }
    public void BubbleSortValue(){
        InventoryRenderer.BubbleSortValue();
    }

    public void PigeonHoleSortOcurrances(){
        InventoryRenderer.PigeonHoleSortOcurrances();
    }

   
}
