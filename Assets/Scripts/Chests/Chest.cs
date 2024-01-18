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

    }

    public void SelectionSort(Dictionary<int, string> keyValuePairs)
    {
        for(int cursor = 0; cursor < keyValuePairs.Count; cursor++)
        {
            KeyValuePair<int, string> lowest;
            lowest = keyValuePairs.ElementAt(cursor);
            for (int i = cursor + 1; i < keyValuePairs.Count; i++)
            {
                if (keyValuePairs.ElementAt(i).Key > lowest.Key)
                {
                    int swap = keyValuePairs.ElementAt(i).Key;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
