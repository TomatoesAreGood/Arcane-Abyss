using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractPanel : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    public bool IsMouseHovering => RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main);
    public GameObject equipButton;
    public GameObject dropButton;
    public GameObject sellButton;
    public GameObject useButton;
    public GameObject equipSpellSlot1Button;
    public GameObject equipSpellSlot2Button;
    public GameObject equipSpellSlot3Button;
    public GameObject equipSpellSlot4Button;
    private void Start(){
        rectTransform = GetComponent<RectTransform>();
    }

    public void OpenPanel(Item item){
        transform.SetParent(transform.root);
        gameObject.SetActive(true);

        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
        if(item is StaffItem){
            equipButton.SetActive(true);
            dropButton.SetActive(true);
        }  

    }




    public void ClosePanel(){
        gameObject.SetActive(false);
    }

    public void ResetPos(){
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        Vector2 mousePos = Input.mousePosition;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x - width/2, mousePos.y - height/2));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

}
