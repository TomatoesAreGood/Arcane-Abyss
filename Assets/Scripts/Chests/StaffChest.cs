using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffChest : Chest
{
    private List<Item> _staffList;
    private Dictionary<Item, int> _staffItemChances;

    // Start is called before the first frame update

    protected override void Start()
    {

        base.Start();
        _sr.color = Color.HSVToRGB(0.3f, 100 / 100, 100 / 100);
        foreach (KeyValuePair<Item, int> kvp in _staffItemChances)
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
        _staffItemChances = new Dictionary<Item, int>();
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
                Debug.Log(ItemLibrary.instance.Library[i].GetType());
                ChestLibrary[chestCursor] = ItemLibrary.instance.Library[i];
                chestCursor++;
            }
        }
    }
}
