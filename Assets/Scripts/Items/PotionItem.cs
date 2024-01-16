using UnityEngine;

public abstract class PotionItem : UsableItem{
    protected override void Awake()
    {
        base.Awake();
        renderer = PlayerController.instance.inventoryUI.potionBagRenderer;
        inventory = PlayerController.instance.inventory.potions;
    }
    public abstract override void Use();
    
}