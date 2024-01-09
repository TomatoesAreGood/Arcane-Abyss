using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Staffs{
    basicStaff
}

public class Staff : MonoBehaviour
{
    public int damageBonus;
    protected Vector3 mousePos;
    protected Transform aimTransform; //pivot
   
    private void SetStaff(Staffs staff){
        
    }

    protected virtual void Start(){
        aimTransform = transform.parent;
        damageBonus = 0;
    }
    
    protected void Update(){
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //orient staff around pivot point --> towards mousePos
        Vector2 aimDirection = mousePos - aimTransform.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0,0,aimAngle);

        //flip staff 
        Vector3 aimLocalScale = Vector3.one;
        if(aimAngle > 90 || aimAngle < -90){
            aimLocalScale.y = -1f;
        }else{
            aimLocalScale.y = 1f;
        }
        aimTransform.localScale = aimLocalScale;
    }


}
