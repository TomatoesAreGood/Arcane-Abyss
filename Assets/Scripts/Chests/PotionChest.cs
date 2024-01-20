using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionChest : Chest
{
    private Item[] _potionArray;
    private int _potionCount;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _sr.color = Color.HSVToRGB(0f, 100 / 100, 100 / 100);

    }

    // Update is called once per frame
    void Update()
    {

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
