using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellBookChest : Chest
{
    private Item[] _bookArray;
    private Dictionary<Item, float> _bookItemChances;
    public TextMeshProUGUI _bookText;


    // Start is called before the first frame update
    protected override void Start()
    {
        LibraryCleanUp();
        PigeonHoleSort();
        _sr.color = Color.HSVToRGB(0.5f, 100 / 100, 100 / 100);
        foreach (KeyValuePair<Item, float> kvp in _bookItemChances)
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
        float dictLength = _bookItemChances.Count;
        _percentString = "";
        foreach (KeyValuePair<Item, float> keyValuePair in _bookItemChances)
        {
            _percentString += $"{keyValuePair.Key} : {(keyValuePair.Value / dictLength) * 100} % \n";
        }
        _bookText.text = _percentString;
    }
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
