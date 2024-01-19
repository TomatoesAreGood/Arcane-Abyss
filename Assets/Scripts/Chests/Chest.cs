using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private int[] keyArray;
    protected string[] _rarityOrder = {"legend", "rare", "common"};
    protected Dictionary<int, string> _keyValuePairs = new Dictionary<int, string>();
    protected string _dropItem;
    // Start is called before the first frame update
    void Start()
    {
        _keyValuePairs.Add(3, "a");
        _keyValuePairs.Add(4, "e");
        _keyValuePairs.Add(1, "b");
        _keyValuePairs.Add(2, "c");
        _keyValuePairs.Add(5, "f");
        _keyValuePairs.Add(6, "d");
        KeySort();
        SelectionSort(keyArray);
        _keyValuePairs = KeyValueAssign(keyArray, _keyValuePairs);
        foreach (KeyValuePair<int, string> kvp in _keyValuePairs)
        {
            Debug.Log(("Key: {0}, Value: {1}", kvp.Key, kvp.Value));
        }

    }

 //First, get a list of dict keys
 //Second, sort the keys
 //Third, create a new dict and assign the newly ordered keys to their values of the old dict

    //First, loop through dictionary to check for keys
    //Input legend keys first, rare keys second, common keys third

    public Dictionary<int, string> RaritySort(Dictionary<int, string> oldDict)
    {
        int rarityCursor = 0;
        string selectKey = _rarityOrder[rarityCursor];
        Dictionary<int, string> newDict = new Dictionary<int, string>();
        for (int i = 0; i < _rarityOrder.Length; i++)
        {
            for (int cursor = 0; cursor < oldDict.Count; cursor++)
            {
                if (oldDict.ElementAt(cursor).Key == 0)// rarity string
                {
                    newDict.Add(oldDict.ElementAt(cursor).Key, oldDict.ElementAt(cursor).Value);
                }
            }
            rarityCursor++;
            selectKey = _rarityOrder[rarityCursor];
        }

        return newDict;
    }
    public void KeySort()
    {
        int cursor = 0;
        keyArray = new int[_keyValuePairs.Count];
        foreach (int key in _keyValuePairs.Keys)
        {
            keyArray[cursor] = key;
            cursor++;
        }
    }
    public void SelectionSort(int[] arr)
    {
        for(int cursor = 0; cursor < arr.Length; cursor++)
        {
            int lowest = cursor;
            for (int i = cursor + 1; i < arr.Length; i++)
            {
                if (arr[i] < arr[lowest])
                {
                    int swap = arr[lowest];
                    arr[lowest] = arr[i];
                    arr[i] = swap;
                }
            }
        }
    }

    public Dictionary<int, string> KeyValueAssign(int[] arr, Dictionary<int, string> oldDict)
    {
        Dictionary<int, string> newDict = new Dictionary<int, string>();
        for (int i = 0; i < arr.Length; ++i)
        {
            for (int cursor = 0; cursor < arr.Length; cursor++)
            {
                if (arr[i] == oldDict.ElementAt(cursor).Key)
                {
                    newDict.Add(oldDict.ElementAt(cursor).Key, oldDict.ElementAt(cursor).Value);
                }
            }
        }

        return newDict;
    }

    public void MagicFindSelect()
    {
        _dropItem = _keyValuePairs.ElementAt(0).Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
