using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionChest : Chest
{
    
    private int _potionCount;
    private Dictionary<Item, float> _potionItemChances;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _sr.color = Color.HSVToRGB(0f, 100 / 100, 100 / 100);
        foreach (KeyValuePair<Item, float> kvp in _potionItemChances)
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
        float dictLength = _potionItemChances.Count;
        _percentString = "";
        foreach (KeyValuePair<Item, float> keyValuePair in _potionItemChances)
        {
            _percentString += $"{keyValuePair.Key} : {(keyValuePair.Value / dictLength) * 100} % \n";

        }
        _chestText.text = _percentString;
    }

    public override void PigeonHoleSort()
    {
        _potionItemChances = new Dictionary<Item, float>();
        for (int i = 0; i < ChestLibrary.Length; i++)
        {
            if (_potionItemChances.ContainsKey(ChestLibrary[i]))
            {
                _potionItemChances[ChestLibrary[i]]++;
            }
            else
            {
                _potionItemChances.Add(ChestLibrary[i], 1);
            }
        }
    }
    
    protected override void LibraryCleanUp()
    {
        foreach (Item item in ItemLibrary.instance.Library)
        {
            if (item is PotionItem)
            {
                _potionCount++;
            }
        }

        ChestLibrary = new Item[_potionCount];

        int chestCursor = 0;
        for (int i = 0; i < ItemLibrary.instance.Library.Length; i++)
        {
            if (ItemLibrary.instance.Library[i] is PotionItem)
            {
                Debug.Log(ItemLibrary.instance.Library[i].GetType());
                ChestLibrary[chestCursor] = ItemLibrary.instance.Library[i];
                chestCursor++;
            }
        }
    }
}
