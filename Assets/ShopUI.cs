using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUI : MonoBehaviour
{
    public GameObject ShopItemPrefab;
    public Item[] shopItems;
    public Transform scrollableList;
    public int numItems;
    public Item[] itemlibrary;
    public virtual void Awake(){
     
        numItems = 10;
        shopItems = new Item[numItems];
        itemlibrary = ItemLibrary.instance.Library;

    }
    private void Start(){
        
        for (int i = 0; i < shopItems.Length; i++){
            int rand = Random.Range(0, itemlibrary.Length);

            while(itemlibrary[rand] is SpellItem){
                rand = Random.Range(0, itemlibrary.Length);
            }

            shopItems[i] = itemlibrary[rand];
        }
        SortByPrice();
        RedrawList();
    }

    public void SortByPrice()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            for(int j = 0; j < shopItems.Length-1; j++)
            {
                if (shopItems[j].value > shopItems[j +1].value)
                {
                    int value = shopItems[j].value;
                    shopItems[j].value = shopItems[j +1].value;
                    shopItems[j +1].value = value;
                }
            }
        }
    }
    
    public void RedrawList(){
        for(int i = 0; i < scrollableList.childCount; i++){
            Destroy(scrollableList.GetChild(i).gameObject);
        }
        for(int i = 0; i < shopItems.Length; i++){
            if(shopItems[i] != null){
                Instantiate(ShopItemPrefab, scrollableList).GetComponent<ShopItem>().itemRef = shopItems[i];
            }
        }
    }

    public void RemoveItem(Item item){
        for(int i = 0; i < shopItems.Length; i++){
            if(shopItems[i] == item){


                shopItems[i] = null;
                break;
                
               
            }
        }
    }

     public void Enable(){
        gameObject.SetActive(true);
    }
    public void Disable(){
        gameObject.SetActive(false);
    }
}
