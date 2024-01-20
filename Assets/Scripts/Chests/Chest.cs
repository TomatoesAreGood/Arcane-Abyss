using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item[] ChestLibrary;
    protected int[] keyArray;
    protected Dictionary<int, string> _keyValuePairs = new Dictionary<int, string>();
    protected string _dropItem;
    // Start is called before the first frame update
    void Start()
    {
        ChestLibrary = ItemLibrary.instance.Library;

/*        KeySort();
        SelectionSort(keyArray);
        _keyValuePairs = KeyValueAssign(keyArray, _keyValuePairs);
        foreach (KeyValuePair<int, string> kvp in _keyValuePairs)
        {
            Debug.Log(("Key: {0}, Value: {1}", kvp.Key, kvp.Value));
        }*/


    }

 //First, get a list of dict keys
 //Second, sort the keys
 //Third, create a new dict and assign the newly ordered keys to their values of the old dict

    public Item RandomSelect()
    {
        int randomIndex = UnityEngine.Random.Range(0, ChestLibrary.Length);
        return ChestLibrary[randomIndex];
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
