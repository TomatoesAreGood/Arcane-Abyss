using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionChest : Chest
{
    private List<Item> _potionList;

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
        _potionList = new List<Item>();
        for (int cursor = 0; cursor < ChestLibrary.Length; cursor++)
        {
            if (ChestLibrary[cursor].GetType() == typeof(PotionItem))
            {
                _potionList.Add(ChestLibrary[cursor]);
            }
        }
    }
}
