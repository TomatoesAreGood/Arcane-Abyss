using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaffChest : Chest
{
    private List<Item> _staffList;
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
        _staffList = new List<Item>();
        for (int cursor = 0; cursor < ChestLibrary.Length; cursor++)
        {
            if (ChestLibrary[cursor].GetType() == typeof(StaffItem))
            {
                _staffList.Add(ChestLibrary[cursor]);
            }
        }
    }
}
