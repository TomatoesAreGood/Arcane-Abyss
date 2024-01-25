using UnityEngine;

public abstract class PotionItem : UsableItem{
    protected override void Awake()
    {
        base.Awake();
        renderer = PlayerController.Instance.inventoryUI.potionBagRenderer;
        inventory = PlayerController.Instance.inventory.potions;
    }
    public override void Use()
    {
        SoundManager.instance.PlaySippingSFX();
    }
    
}