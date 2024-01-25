using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour{
    public static PickUpController instance;
    public bool canPickUp;
    public bool isPickingUp;
    [SerializeField] GameObject pickUpPrompt;
    public GameObject defaultDropItem;


    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }

        canPickUp = false;
    }

    private void Update()
    {
        if (canPickUp){
            pickUpPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F)){
                isPickingUp = true;
            }
        }else{
            pickUpPrompt.SetActive(false);
        }
            
        canPickUp = false;
    }
    
    public bool TryPickUp(Item item){
        Inventory inventory = PlayerController.Instance.inventory;
        PlayerController playerController = PlayerController.Instance;
        playerController.inventoryUI.UpdateData();

        if(item.ItemType == Renderers.inventory){
            for(int i = 0; i < inventory.Items.Length; i++){
                if(inventory.Items[i] == null){
                    playerController.inventoryUI.InventoryRenderer.InstantiateItem(item,i);
                    return true;
                }
            }
        }else if(item.ItemType == Renderers.potion){
            for(int i = 0; i < inventory.Potions.Length; i++){
                if(inventory.Potions[i] == null){
                    playerController.inventoryUI.PotionBagRenderer.InstantiateItem(item,i);
                    return true;
                }
            }
        }
        
        return false;
    }

    public bool TryAddSpell(SpellItem item){
        PlayerController playerController = PlayerController.Instance;
        Inventory inventory = PlayerController.Instance.inventory;

        if(item.ItemType == Renderers.spells){
            if(playerController.FindSpellInInventory(item) > 0){
                return false;
            }
            for(int i = 0; i < inventory.Spells.Length; i++){
                if(inventory.Spells[i] == null){
                    playerController.inventoryUI.SpellsRenderer.InstantiateItem(item,i);
                    playerController.inventoryUI.SpellsRenderer.UpdateData();
                    return true;
                }
            }
        }
        return false;
    }
}
