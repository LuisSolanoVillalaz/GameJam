using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
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
        block.velocity = new Vector3(0, block.velocity.y, 0);
         if(isMoving){
                block.MovePosition( Vector3.MoveTowards(block.position,direc,0.2f));
                if(block.position==direc){
                    isMoving=false;
                }
                
            }
           
    }
   
    public bool PushTo(float d,Vector3 v){
        bool moved=false;

        Vector3 direction=v;
      

        //Test for collision in next movement
        Collider[] colobj=Physics.OverlapBox(
            block.position+d*direction, (transform.localScale/2)-
            new Vector3(microdist,microdist,microdist)
        );
        Collider[] parent=Physics.OverlapBox(
                block.position+d*Vector3.up, (transform.localScale/2)-
                new Vector3(microdist,microdist,microdist)
                );
        if(colobj.Length == 1){
            bool succes=false;
            if(colobj[0].tag=="Box"){
                Push pushblock =colobj[0].gameObject.GetComponent<Push>();
                succes=pushblock.PushTo(d,direction);
            }
            if (succes==true||colobj[0].tag=="Player"){
                if(parent.Length == 1){
                if(parent[0].tag=="Box"){
                    Push pushblock =parent[0].gameObject.GetComponent<Push>();
                    pushblock.PushTo(d,direction);
                }}
                direc=block.position +(d*direction);
                isMoving=true;
                moved=true;
            }
            
        }else if(colobj.Length == 0){
            if(parent.Length == 1){
            if(parent[0].tag=="Box"){
                Push pushblock =parent[0].gameObject.GetComponent<Push>();
                pushblock.PushTo(d,direction);
            }}
            direc=block.position +(d*direction);
            
            isMoving=true;
            moved=true;
        }

        
       
        return moved;
    }
}
