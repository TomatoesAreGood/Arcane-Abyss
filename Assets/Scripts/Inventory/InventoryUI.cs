using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventoryRenderer inventoryRenderer;
    public InventoryRenderer potionBagRenderer;
    public InventoryRenderer spellsRenderer;

    public bool isOpen;
    [SerializeField] Image equippedStaff;
    [SerializeField] Image equippedSpell1;
    [SerializeField] Image equippedSpell2;
    [SerializeField] Image equippedSpell3;
    [SerializeField] Image equippedSpell4;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    private void Update() {    
        if (isOpen){
            equippedStaff.sprite = PlayerController.instance.equippedStaff.GetComponent<SpriteRenderer>().sprite;
            
            equippedSpell1.sprite = PlayerController.instance.inventory.equippedSpells[0].reference.GetComponent<SpriteRenderer>().sprite;
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
