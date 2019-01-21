using UnityEngine;
using UnityEngine.AI;

public class ArtyClass : MonoBehaviour {
//參考:https://forum.unity.com/threads/artillery-ballistics-gravity.41920/
    int damage{get;set;}
    float blaset{get;set;}
    float gravity{get;set;}
    float force{get;set;}
    float curAngle{get;set;}
    Vector3 targetPos{get;set;}

    void Start()
    {        
        var rig = this.GetComponent<Rigidbody>();
        this.gravity = rig.drag;
        this.force = 10f;    

        var aimDist = Vector3.Distance(transform.position, targetPos);
        var groundOffset = Mathf.Abs(targetPos.y - transform.position.y) * 1.5f;
        this.curAngle = Mathf.Rad2Deg * (Mathf.Asin(Mathf.Min(((aimDist + groundOffset) * this.gravity)/Mathf.Pow(this.force/100, 2), 1))/2);        
        //"轉"子彈的角度
        rig.rotation = Quaternion.AngleAxis(curAngle,Vector3.up);
    }
}