using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUI : MonoBehaviour
{
    public GameObject ShopItemPrefab;
    public Item[] _shopItems;
    public Transform scrollableList;
    private int _numItems;
    public Item[] _itemlibrary;

    public virtual void Awake()
    {
        // defines size of shop 
        _numItems = 20;
        _shopItems = new Item[_numItems];
        _itemlibrary = ItemLibrary.instance.Library;

    }
    private void Start()
    {
        // Fills up the shop with random items until the array is full
        for (int i = 0; i < _shopItems.Length; i++)
        {
            int rand = Random.Range(0, _itemlibrary.Length);
            // spell items are exempt due to being sold through books
            while (_itemlibrary[rand] is SpellItem)
            {
                rand = Random.Range(0, _itemlibrary.Length);
            }

            _shopItems[i] = _itemlibrary[rand];

        }
        // displays the shop items
        RedrawList();
    }
    //Sorts By Price
    public void SortByPrice()
    {
        for (int i = 0; i < _shopItems.Length; i++)
        {
            for (int j = 0; j < _shopItems.Length - 1; j++)
            {

                if (ShopItem.itemPrices[_shopItems[j].GetType().Name] > ShopItem.itemPrices[_shopItems[j + 1].GetType().Name])
                {
                    Item value = _shopItems[j];
                    _shopItems[j] = _shopItems[j + 1];
                    _shopItems[j + 1] = value;
                }
            }
        }
        RedrawList();

    }

    public void MergeSort()
    {
        MergeSortSortAlphabetically(_shopItems);
        RedrawList();
    }
    //Sorts Array Alphebetically 
    public void MergeSortSortAlphabetically(Item[] Array)
    {

        if (Array.Length <= 1)
        {
            return;
        }

        int Midpoint = Array.Length / 2;

        Item[] LeftArray = new Item[Midpoint];
        Item[] RightArray = new Item[Array.Length - LeftArray.Length];

        for (int i = 0; i < LeftArray.Length; i++)
        {
            LeftArray[i] = Array[i];
        }

        for (int j = 0; j < RightArray.Length; j++)
        {
            RightArray[j] = Array[LeftArray.Length + j];
        }
        MergeSortSortAlphabetically(LeftArray);
        MergeSortSortAlphabetically(RightArray);


        int Cursor = 0; int LeftMarker = 0; int RightMarker = 0;

        while (LeftMarker < LeftArray.Length && RightMarker < RightArray.Length)
        {
            if (LeftArray[LeftMarker].name.CompareTo(RightArray[RightMarker].name) < 0)
            {
                Array[Cursor] = LeftArray[LeftMarker]; Cursor++; LeftMarker++;

            }
            else
            {
                Array[Cursor] = RightArray[RightMarker]; Cursor++; RightMarker++;

            }
        }

        while (LeftMarker < LeftArray.Length)
        {
            Array[Cursor] = LeftArray[LeftMarker]; Cursor++; LeftMarker++;
        }
        while (RightMarker < RightArray.Length)
        {
            Array[Cursor] = RightArray[RightMarker]; Cursor++; RightMarker++;

        }

    }



    public void RedrawList()
    {
        // Destroys all current objects attatched to the list
        for (int i = 0; i < scrollableList.childCount; i++)
        {
            Destroy(scrollableList.GetChild(i).gameObject);
        }
        // redisplays all the shop items 
        for (int i = 0; i < _shopItems.Length; i++)
        {
            if (_shopItems[i] != null)
            {
                Instantiate(ShopItemPrefab, scrollableList).GetComponent<ShopItem>().itemRef = _shopItems[i];
            }
        }
    }

    public void RemoveItem(Item item)
    {
        // removes shop item when it is clicked and when player has enough money
        for (int i = 0; i < _shopItems.Length; i++)
        {
            if (_shopItems[i] == item)
            {

                _shopItems[i] = null;
                break;


            }
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void BackToGame()
    {
        ShopManager.Instance.SetPauseStatus(false);
        gameObject.SetActive(false);
        PauseManager.instance.Resume();
    }


}
