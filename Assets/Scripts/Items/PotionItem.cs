using UnityEngine;

public abstract class PotionItem : UsableItem{
    protected override void Awake()
    {
        base.Awake();
        Renderer = PlayerController.Instance.inventoryUI.PotionBagRenderer;
        Inventory = PlayerController.Instance.inventory.Potions;
    }
    public override void Use()
    {
        SoundManager.instance.PlaySippingSFX();
    }
    
}