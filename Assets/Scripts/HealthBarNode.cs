using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarNode : MonoBehaviour
{
    public Sprite HeartSprite;
    public GameObject Next;
    public GameObject Previous;

    // Start is called before the first frame update
    public HealthBarNode(Sprite sprite)
    {
        this.HeartSprite = sprite;
        this.Next = null;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {

    }
}
