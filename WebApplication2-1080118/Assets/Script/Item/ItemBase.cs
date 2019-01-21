using UnityEngine;

class ItemBase:MonoBehaviour
{
    public string name{get;set;}
    public string pickUpMessage{get;set;}
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "player")
        {
            
        }    
    }
}