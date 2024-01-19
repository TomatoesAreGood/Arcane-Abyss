using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chest : MonoBehaviour
{

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
        SelectionSort(keyValuePairs);
        foreach (KeyValuePair<int, string> kvp in keyValuePairs)
        {
            Debug.Log(("Key: {0}, Value: {1}", kvp.Key, kvp.Value));
        }

    }

    public void SelectionSortRevised(Dictionary<int, string> keyValuePairs)
    {

    }

    public void SelectionSort(Dictionary<int, string> keyValuePairs)
    {
        for(int cursor = 0; cursor < keyValuePairs.Count; cursor++)
        {
            KeyValuePair<int, string> lowest;
            lowest = keyValuePairs.ElementAt(cursor);
            Debug.Log("lowest: " + lowest);
            for (int i = cursor + 1; i < keyValuePairs.Count; i++)
            {
                Debug.Log("inspected: " + keyValuePairs.ElementAt(i));
                KeyValuePair<int, string> inspect = keyValuePairs.ElementAt(i);
                if (inspect.Key < lowest.Key)
                {
                    int toKey = lowest.Key;
                    string toValue = lowest.Value;
                    int fromKey = inspect.Key;
                    string fromValue = inspect.Value;

                    keyValuePairs[toKey] = inspect.Value;




/*                    int toKey = keyValuePairs.ElementAt(i).Key;
                    string fromKeyValue = keyValuePairs[lowest.Key];
                    keyValuePairs.Remove(lowest.Key);
                    keyValuePairs[toKey] = fromKeyValue;

                    int fromKey = lowest.Key;
                    string toKeyValue = keyValuePairs[keyValuePairs.ElementAt(i).Key];
                    keyValuePairs.Remove(keyValuePairs.ElementAt(i).Key);
                    keyValuePairs[fromKey] = toKeyValue;*/

                }
                Debug.Log("Finished inner looop");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
