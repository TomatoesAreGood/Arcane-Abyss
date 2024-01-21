using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookChest : Chest
{
    private Item[] _bookArray;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _sr.color = Color.HSVToRGB(0.5f, 100 / 100, 100 / 100);

    }

    // Update is called once per frame
    void Update()
    {
        
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
