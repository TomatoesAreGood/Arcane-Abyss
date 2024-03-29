using Pathfinding.Ionic.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Chest : MonoBehaviour
{
    protected SpriteRenderer _sr;
    protected string _percentString;
    protected List<Item> _pigeonItems;
    protected string _dropItem;
    protected bool _isLocked = false;

    private int _invalidCount;
    private Dictionary<string, float> _itemChances;
    private string[] _itemTypes = { "Staff", "SpellBook", "Potion" };

    public Item[] ChestLibrary;
    public Sprite OpenSprite;
    public Canvas Canvas;
    public TextMeshProUGUI ChestText;
    

    protected virtual void Awake()
    {
        Canvas.worldCamera = Camera.main;

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _percentString = "";

        LibraryCleanUp();
        PigeonHoleSort();
        RandomizeLock();

        SetChestText();
        _sr = GetComponent<SpriteRenderer>();
/*        foreach (KeyValuePair<string, float> kvp in _itemChances)
        {
            Debug.Log(("Key: {0}, Value: {1}", kvp.Key, kvp.Value));
        }*/



        /*        KeySort();
                SelectionSort(keyArray);
                _keyValuePairs = KeyValueAssign(keyArray, _keyValuePairs);*/
    }

    //First, get a list of dict keys
    //Second, sort the keys
    //Third, create a new dict and assign the newly ordered keys to their values of the old dict

    //sort item types(staff, spellbook, potion) in # of frequincies with pigeonhole sort

    protected void RandomizeLock()
    {
        int randomNum = UnityEngine.Random.Range(1, 7);
        if (randomNum == 1) { 
            _isLocked = true;
            _percentString += "****LOCKED****\n";
        }
    }

    protected virtual void SetChestText()
    {
        float dictLength = 0;
        foreach (int value in _itemChances.Values)
        {
            dictLength += value;
        }
        foreach (KeyValuePair<string, float> keyValuePair in _itemChances)
        {
            _percentString += $"{keyValuePair.Key} : {Mathf.Round((keyValuePair.Value / dictLength) * 100)} % \n";
        }
        ChestText.text = _percentString;
    }

    //pigeonhole sort to sort throw an array of Items and add them to a Dictionary<Item, int> sorting by frequincy Items
    public virtual void PigeonHoleSort()
    {
        _itemChances = new Dictionary<string, float>();
        Dictionary<string, float> tempDict = new Dictionary<string, float>(); 
        for (int j = 0; j < _itemTypes.Length; j++)
        {
            for (int i = 0; i < ChestLibrary.Length; i++)
            {
                if (FindName(ChestLibrary[i].GetType().Name, _itemTypes[j]))
                {
                    if (_itemChances.ContainsKey(_itemTypes[j]))
                    {
                        _itemChances[_itemTypes[j]]++;
                    }
                    else
                    {
/*                        Debug.Log("Added key");
*/                        _itemChances.Add(_itemTypes[j], 1);
                    }
                }
            }
        }

        while(_itemChances.Count > 0)
        {
            float max = -1;
            string key = "";
                foreach (KeyValuePair<string, float> kvp in _itemChances)
                {
                    if (kvp.Value > max)
                    {
                        max = kvp.Value;
                        key = kvp.Key;
                    }
                }
            tempDict.Add(key, max);
            _itemChances.Remove(key);
        }
        _itemChances = tempDict;
        Debug.Log(_itemChances);
    }

        

    //Recursive Loop to find a specific substring from a string
    public bool FindName(string item, string target)
    {
        if (item.Length < target.Length)
        {
            return false;
        }

        if (item.Substring(0, target.Length) == target)
        {
            return true;
        }

       
        else
        {
            return FindName(item.Substring(1), target);
        }
    }
    //change sprite to open chest and drop item
    public void Open()
    {
        _sr.sprite = OpenSprite;
        RandomSelect().Drop(transform.position);
        Destroy(gameObject);
    }

    //removes objects with inherited class "SpellItem"
    protected virtual void LibraryCleanUp()
    {
        foreach (Item item in ItemLibrary.instance.Library)
        {
            if (item is SpellItem)
            {
                _invalidCount++;
            }
        }

        ChestLibrary = new Item[ItemLibrary.instance.Library.Length - _invalidCount];

        int chestCursor = 0;
        for (int i = 0; i < ItemLibrary.instance.Library.Length; i++)
        {
            if (!(ItemLibrary.instance.Library[i] is SpellItem))
            {
/*                Debug.Log(ItemLibrary.instance.Library[i].GetType());
*/                ChestLibrary[chestCursor] = ItemLibrary.instance.Library[i];
                  chestCursor++;
            }
        }
    }

    protected virtual Item RandomSelect()
    {
        int randomIndex = UnityEngine.Random.Range(0, ChestLibrary.Length);
        return ChestLibrary[randomIndex];
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerIsTrigger")) {
            if (_isLocked == true && PlayerController.Instance.HasKey())
            {
                Open();
                PlayerController.Instance.RemoveKey();
                SoundManager.instance.PlayOpenLockedChestSFX();
            }
            else if (_isLocked == false)
            {
                Open();
                SoundManager.instance.PlayOpenUnlockedChestSFX();
            }
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
