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
        int numActiveButtons = 0;
        transform.SetParent(transform.root);
        gameObject.SetActive(true);

        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if(item is StaffItem){
            dropButton.SetActive(true);
            equipButton.SetActive(true);
            numActiveButtons = 2;
        }  
        else if(item is SpellItem){
            equipSpellSlot1Button.SetActive(true);
            equipSpellSlot2Button.SetActive(true);
            equipSpellSlot3Button.SetActive(true);
            equipSpellSlot4Button.SetActive(true);
            numActiveButtons = 4;
        } else if(item is UsableItem){
            dropButton.SetActive(true);
            useButton.SetActive(true);
            numActiveButtons = 2;
        } 
        rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, numActiveButtons*30f);
        ResetPos();
        

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
