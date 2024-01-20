using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookChest : Chest
{
    private List<Item> _bookList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StaffSort()
    {
        _bookList = new List<Item>();
        for (int cursor = 0; cursor < ChestLibrary.Length; cursor++)
        {
            if (ChestLibrary[cursor].GetType() == typeof(SpellBook))
            {
                _bookList.Add(ChestLibrary[cursor]);
            }
        }
    }
}
