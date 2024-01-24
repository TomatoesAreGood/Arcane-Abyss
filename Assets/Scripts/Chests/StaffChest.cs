using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StaffChest : Chest
{
    private Dictionary<string, float> _staffItemChances;


    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();

        _sr.color = Color.HSVToRGB(0.8f, 100 / 100, 100 / 100);
        foreach (KeyValuePair<string, float> kvp in _staffItemChances)
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
        foreach (KeyValuePair<string, float> keyValuePair in _staffItemChances)
        {
            _percentString += $"{keyValuePair.Key} : {Mathf.Round((keyValuePair.Value / dictLength) * 100)} % \n";

        }
        ChestText.text = _percentString;
    }

    //pigeonhole sort to sort throw an array of Items and add them to a Dictionary<Item, int> sorting by frequincy Items
    public override void PigeonHoleSort()
    {
        _staffItemChances = new Dictionary<string, float>();
        Dictionary<string, float> tempDict = new Dictionary<string, float>();

        for (int i = 0; i < ChestLibrary.Length; i++)
        {
            if (_staffItemChances.ContainsKey(ChestLibrary[i].GetType().Name))
            {
                _staffItemChances[ChestLibrary[i].GetType().Name]++;
            }
            else
            {
                _staffItemChances.Add(ChestLibrary[i].GetType().Name, 1);
            }
        }
        while (_staffItemChances.Count > 0)
        {
            float max = -1;
            string key = "";
            foreach (KeyValuePair<string, float> kvp in _staffItemChances)
            {
                if (kvp.Value > max)
                {
                    max = kvp.Value;
                    key = kvp.Key;
                }
            }
            tempDict.Add(key, max);
            _staffItemChances.Remove(key);
        }
        _staffItemChances = tempDict;
        Debug.Log(_staffItemChances);
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
