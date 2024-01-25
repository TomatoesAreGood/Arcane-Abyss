using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI desc;
    [SerializeField] TextMeshProUGUI value;

    // Start is called before the first frame update
    private void Start(){
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public void UpdateInfo(string title, string desc, int value)
    {
        this.title.text = title;
        this.desc.text = desc;
        this.value.text = "Value: " + value + " Coins";
    }

    public void ResetPos(){
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        Vector2 mousePos = Input.mousePosition;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x - width/2, mousePos.y - height/2));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
    public void ClosePanel(){
        gameObject.SetActive(false);
    }

    public void OpenPanel(Item item){
        UpdateInfo(item.title, item.desc, item.value);
        ResetPos();
        gameObject.SetActive(true);
    }

}
