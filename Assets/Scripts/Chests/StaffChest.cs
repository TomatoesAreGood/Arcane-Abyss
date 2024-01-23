using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StaffChest : Chest
{
    private Dictionary<Item, float> _staffItemChances;


    // Start is called before the first frame update

    protected override void Start()
    {

        LibraryCleanUp();
        PigeonHoleSort();
        SetChestText();
/*        _sr.color = Color.HSVToRGB(0.3f, 100 / 100, 100 / 100);*/
        foreach (KeyValuePair<Item, float> kvp in _staffItemChances)
        {
            Debug.Log(("Key: {0}, Value: {1}", kvp.Key, kvp.Value));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void SetChestText()
    {
        float dictLength = _staffItemChances.Count;
        _percentString = "";
        foreach (KeyValuePair<Item, float> keyValuePair in _staffItemChances)
        {
            _percentString += $"{keyValuePair.Key} : {(keyValuePair.Value / dictLength) * 100} % \n";

        }
        _chestText.text = _percentString;
    }

    //pigeonhole sort to sort throw an array of Items and add them to a Dictionary<Item, int> sorting by frequincy Items
    public override void PigeonHoleSort()
    {
        _staffItemChances = new Dictionary<Item, float>();
        for (int i = 0; i < ChestLibrary.Length; i++)
        {
            if (_staffItemChances.ContainsKey(ChestLibrary[i]))
            {
                _staffItemChances[ChestLibrary[i]]++;
            }
            else
            {
                _staffItemChances.Add(ChestLibrary[i], 1);
            }
        }
    }
    //search through the ItemLibrary array of Items and add Items that derive from StaffItem to a Item Array
    protected override void LibraryCleanUp()
    {
        int bookCount = 0;
        foreach (Item item in ItemLibrary.instance.Library)
        {
            if (item is StaffItem)
            {
                bookCount++;
            }
        }

        ChestLibrary = new Item[bookCount];

        int chestCursor = 0;
        for (int i = 0; i < ItemLibrary.instance.Library.Length; i++)
        {
            if (ItemLibrary.instance.Library[i] is StaffItem)
            {
/*                Debug.Log(ItemLibrary.instance.Library[i].GetType());
*/                ChestLibrary[chestCursor] = ItemLibrary.instance.Library[i];
                chestCursor++;
            }
        }
    }
}
