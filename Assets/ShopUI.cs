using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject ShopItemPrefab;
    public Item[] shopItems;
    public Transform scrollableList;
    private int numItems;

    private void Awake(){
     
        numItems = 10;
        shopItems = new Item[numItems];
    }
    private void Start(){
        Item[] itemlibrary = ItemLibrary.instance.Library;
        for(int i = 0; i < shopItems.Length; i++){
            int rand = Random.Range(0, itemlibrary.Length);

            while(itemlibrary[rand] is SpellItem){
                rand = Random.Range(0, itemlibrary.Length);
            }

            shopItems[i] = itemlibrary[rand];
        }
        RedrawList();
    }
    
    public void RedrawList(){
        string a = "";

        foreach(Item item in shopItems){
            if(item != null){
                a+= item + ", ";
            }else{
                a += "  ";
            }
        }
        Debug.Log(a);

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
