using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionChest : Chest
{
    
    private int _potionCount;
    private Dictionary<string, float> _potionItemChances;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _sr.color = Color.HSVToRGB(0f, 100 / 100, 100 / 100);
        foreach (KeyValuePair<string, float> kvp in _potionItemChances)
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
        foreach (KeyValuePair<string, float> keyValuePair in _potionItemChances)
        {
            _percentString += $"{keyValuePair.Key} : {Mathf.Round((keyValuePair.Value / dictLength) * 100)} % \n";
        }
        _chestText.text = _percentString;
    }


    //pigeonhole sort to sort throw an array of Items and add them to a Dictionary<Item, int> sorting by frequincy Items
    public override void PigeonHoleSort()
    {
        _potionItemChances = new Dictionary<string, float>();
        Dictionary<string, float> tempDict = new Dictionary<string, float>();

        for (int i = 0; i < ChestLibrary.Length; i++)
        {
            if (_potionItemChances.ContainsKey(ChestLibrary[i].GetType().Name))
            {
                _potionItemChances[ChestLibrary[i].GetType().Name]++;
            }
            else
            {
                _potionItemChances.Add(ChestLibrary[i].GetType().Name, 1);
            }
        }
        while (_potionItemChances.Count > 0)
        {
            float max = -1;
            string key = "";
            foreach (KeyValuePair<string, float> kvp in _potionItemChances)
            {
                if (kvp.Value > max)
                {
                    max = kvp.Value;
                    key = kvp.Key;
                }
            }
            tempDict.Add(key, max);
            _potionItemChances.Remove(key);
        }
        _potionItemChances = tempDict;
        Debug.Log(_potionItemChances);
    }

    //search through the ItemLibrary array of Items and add Items that derive from PotionItem to a Item Array
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
