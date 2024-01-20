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
        Inventory inventory = PlayerController.instance.inventory;
        PlayerController playerController = PlayerController.instance;
        playerController.inventoryUI.UpdateData();

        if(item.itemType == Renderers.inventory){
            for(int i = 0; i < inventory.items.Length; i++){
                if(inventory.items[i] == null){
                    playerController.inventoryUI.inventoryRenderer.InstantiateItem(item,i);
                    return true;
                }
            }
        }else if(item.itemType == Renderers.potion){
            for(int i = 0; i < inventory.potions.Length; i++){
                if(inventory.potions[i] == null){
                    playerController.inventoryUI.potionBagRenderer.InstantiateItem(item,i);
                    return true;
                }
            }
        }
        
        return false;
    }

    public bool TryAddSpell(SpellItem item){
        PlayerController playerController = PlayerController.instance;
        Inventory inventory = PlayerController.instance.inventory;

        if(item.itemType == Renderers.spells){
            if(playerController.FindEquippedSpell(item) < 1){
                return false;
            }
            for(int i = 0; i < inventory.spells.Length; i++){
                if(inventory.spells[i] == null){
                    playerController.inventoryUI.spellsRenderer.InstantiateItem(item,i);
                    return true;
                }
            }
        }
        return false;
    }
}
