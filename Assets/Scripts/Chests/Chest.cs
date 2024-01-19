using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private int[] keyArray;

    protected Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
    // Start is called before the first frame update
    void Start()
    {
        keyValuePairs.Add(3, "a");
        keyValuePairs.Add(4, "e");
        keyValuePairs.Add(1, "b");
        keyValuePairs.Add(2, "c");
        keyValuePairs.Add(5, "f");
        keyValuePairs.Add(6, "d");
        KeySort();
        SelectionSort(keyArray);
        foreach (KeyValuePair<int, string> kvp in KeyValueAssign(keyArray, keyValuePairs)
)
        {
            Debug.Log(("Key: {0}, Value: {1}", kvp.Key, kvp.Value));
        }

    }

 //First, get a list of dict keys
 //Second, sort the keys
 //Third, create a new dict and assign the newly ordered keys to their values of the old dict

    public void KeySort()
    {
        int cursor = 0;
        keyArray = new int[keyValuePairs.Count];
        foreach (int key in keyValuePairs.Keys)
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
