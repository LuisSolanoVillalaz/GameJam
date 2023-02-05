using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushFree : MonoBehaviour
{ 
     Rigidbody block;
    float halfColliderWidth;
    float halfColliderHight;
    float microdist=0.01f;

    Vector3 Pos;
    Vector3 direc;
    bool isMoving=false;
    
      void Start()
    {
        //Get size of collider
        block= gameObject.GetComponent<Rigidbody>();
        halfColliderWidth = GetComponent<BoxCollider>().size.x / 2;
        halfColliderHight = GetComponent<BoxCollider>().size.y / 2;

    }
 
   
    void FixedUpdate(){
      
        
        /*if(col.gameObject.tag=="Box"){
            if(transform.position.y-col.transform.position.y>0.8f && Mathf.Abs(transform.position.x-col.transform.position.x)<0.8f&&Mathf.Abs(transform.position.z-col.transform.position.z)<0.8f){
                transform.position= new Vector3(col.transform.position.x,transform.position.y,col.transform.position.z);
            }
        }*/
    }
}
