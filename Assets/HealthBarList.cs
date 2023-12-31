using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarList : MonoBehaviour
{

    //attriutes for keeping tract of place on linked list
    public GameObject StartNode;
    public GameObject EndNode;
    public GameObject HealthHeart;
    private GameObject _firstFullHeart;

    //attributes used for changing heart sprites
    private SpriteRenderer sr;
    private Image _imageSprite;
    public Sprite EmptyHeart;
    public Sprite FullHeart;

    HealthBarNode endNodeClass;
    HealthBarNode newNodeClass;
    HealthBarNode _firstHeartClass;
    // Start is called before the first frame update


    private void Start()
    {
        InstantiateFullHeart();
        InstantiateFullHeart();
        InstantiateFullHeart();
        EmptyFullHeart();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //instantiate a full heart sprite into the horizontal layout group; used when player can increase max health ingame
    public void InstantiateFullHeart()
    {
        GameObject newHeart = Instantiate(HealthHeart, gameObject.transform);
        if (this.StartNode == null)
        {
            this.StartNode = newHeart;
            _firstFullHeart = StartNode;
            this.EndNode = newHeart;
        }
        else
        {
            endNodeClass = EndNode.GetComponent<HealthBarNode>();
            newNodeClass = newHeart.GetComponent<HealthBarNode>();
            _firstHeartClass = GetComponent<HealthBarNode>();

            //if there is a start node present, connect the end node gameobject "Next" to the new gameobject,
            //connect the new gameobject "Previous" to the end node gameobject, and finally set the end node to the new gameobject

            newNodeClass.Previous = EndNode;

            endNodeClass.Next = newHeart;

            EndNode = newHeart;
        }

        _firstFullHeart = newHeart;
    }

    public void EmptyFullHeart()
    {
        _imageSprite = _firstFullHeart.GetComponent<Image>();
        _imageSprite.sprite = EmptyHeart;

        _firstHeartClass = _firstFullHeart.GetComponent<HealthBarNode>();
        if (_firstHeartClass.Previous != null)
        {
            _firstFullHeart = _firstHeartClass.Previous;
        }

        ChangeColor();


    }

    public void FillEmptyHeart()
    {

        _imageSprite = _firstFullHeart.GetComponent<Image>();
        _imageSprite.sprite = FullHeart;

        _firstHeartClass = _firstFullHeart.GetComponent<HealthBarNode>();
        if (_firstHeartClass.Next != null) {
            _firstFullHeart = _firstHeartClass.Next;
        }

    }

    public IEnumerator ChangeColor()
    {
        Debug.Log("color");
        sr.color = Color.red;
        yield return new WaitForSeconds(0.20f);
        sr.color = Color.white;
    }
}
