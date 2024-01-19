using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    protected Vector3 mousePos;

    // Update is called once per frame
    private void Update(){
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Transform aimTransform = transform;
        
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
