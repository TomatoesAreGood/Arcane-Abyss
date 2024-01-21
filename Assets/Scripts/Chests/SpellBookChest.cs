using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookChest : Chest
{
    private Item[] _bookArray;
    private Dictionary<Item, int> _bookItemChances;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _sr.color = Color.HSVToRGB(0.5f, 100 / 100, 100 / 100);
        foreach (KeyValuePair<Item, int> kvp in _bookItemChances)
        {
            Debug.Log(("Key: {0}, Value: {1}", kvp.Key, kvp.Value));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void PigeonHoleSort()
    {
        _bookItemChances = new Dictionary<Item, int>();
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
                Debug.Log(ItemLibrary.instance.Library[i].GetType());
                ChestLibrary[chestCursor] = ItemLibrary.instance.Library[i];
                chestCursor++;
            }
        }
    }
}
