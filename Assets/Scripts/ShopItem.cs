using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public static Dictionary<string, int> itemPrices = new Dictionary<string, int>{
        {"BasicStaff", 10}, {"ForestStaff", 50},{"HealthPotion", 20},{"DarkStaff", 500},
        {"FireShotSpellBook", 200},{"IceShotSpellBook", 200},{"WindShotSpellBook", 200},{"SmallHealthPotion", 10},
        {"SmallManaPotion", 15},{"ManaPotion", 25},{"IceStaff", 85},{"DemonicEyeStaff", 100},{"HolyStaff", 100},{"UndeadStaff", 85},{"OrbOfHoarding",300},
        {"TridentStaff", 300}, {"Key", 50}
    };
    public bool IsMouseOnItem => RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main);
    private RectTransform rectTransform;
    public Item itemRef;
    private Image iconImage;
    private Image image;
    private TextMeshProUGUI text;
    private TextMeshProUGUI price;

    private void Awake(){
        image = GetComponent<Image>();
        iconImage = transform.GetChild(0).GetComponent<Image>();
        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        price = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start(){
        iconImage.sprite = itemRef.GetComponent<Image>().sprite;
        text.text = itemRef.GetType().Name;
        price.text = "" + itemPrices[itemRef.GetType().Name];
    }
    private void Update(){
        if(IsMouseOnItem){
            SetAlpha(0.75f);
        }else{
            SetAlpha(1f);
        }

        if(IsMouseOnItem && Input.GetMouseButtonDown(0)){
            if(PlayerController.Instance.Coins > itemPrices[itemRef.GetType().Name])
            {

                if (PickUpController.instance.TryPickUp(itemRef))
                {
                    PlayerController.Instance.Coins -= itemPrices[itemRef.GetType().Name];
                    Debug.Log("bought item: " + itemRef.ToString());
                    ShopManager.Instance.ShopUI.RemoveItem(itemRef);
                    ShopManager.Instance.ShopUI.RedrawList();
                }
            }
               
        }
    }
      private void SetAlpha(float alpha){
        var tempColor = iconImage.color;
        tempColor.a = alpha;
        iconImage.color = tempColor;
        tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }
   


    
}
