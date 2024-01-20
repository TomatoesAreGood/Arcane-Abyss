using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffChest : Chest
{
    private List<Item> _staffList;
    // Start is called before the first frame update

    protected override void Start()
    {

        base.Start();
        float HValue = 120 / 360;
        _sr.color = Color.HSVToRGB(0.3f, 100 / 100, 100 / 100);

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
