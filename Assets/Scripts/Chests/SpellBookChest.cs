using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellBookChest : Chest
{
    private Dictionary<Item, float> _bookItemChances;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _sr.color = Color.HSVToRGB(0.5f, 100 / 100, 100 / 100);
        foreach (KeyValuePair<Item, float> kvp in _bookItemChances)
        {
            Debug.Log(("Key: {0}, Value: {1}", kvp.Key, kvp.Value));
        }
    }

    protected override void SetChestText()
    {
        float dictLength = _bookItemChances.Count;
        _percentString = "";
        foreach (KeyValuePair<Item, float> keyValuePair in _bookItemChances)
        {
            _percentString += $"{keyValuePair.Key} : {(keyValuePair.Value / dictLength) * 100} % \n";

        }
        _chestText.text = _percentString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //pigeonhole sort to sort throw an array of Items and add them to a Dictionary<Item, int> sorting by frequincy Items
    public override void PigeonHoleSort()
    {
        _bookItemChances = new Dictionary<Item, float>();
        for (int i = 0; i < ChestLibrary.Length; i++)
        {
            if (_bookItemChances.ContainsKey(ChestLibrary[i]))
            {
                _bookItemChances[ChestLibrary[i]]++;
            }
            else
            {
                _bookItemChances.Add(ChestLibrary[i], 1);
            }
        }
    }

    //search through the ItemLibrary array of Items and add Items that derive from SpellBook to a Item Array
    protected override void LibraryCleanUp()
    {
        int bookCount = 0;
        foreach (Item item in ItemLibrary.instance.Library)
        {
            if (item is SpellBook)
            {
                bookCount++;
            }
        }

        ChestLibrary = new Item[bookCount];

        int chestCursor = 0;
        for (int i = 0; i < ItemLibrary.instance.Library.Length; i++)
        {
            if (ItemLibrary.instance.Library[i] is SpellBook)
            {
                //Debug.Log(ItemLibrary.instance.Library[i].GetType());
                ChestLibrary[chestCursor] = ItemLibrary.instance.Library[i];
                chestCursor++;
            }
        }
    }
}
