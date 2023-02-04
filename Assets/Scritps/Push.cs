using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{ 
     Rigidbody block;
    float halfColliderWidth;
    float halfColliderHight;
    float microdist=0.01f;
      void Start()
    {
        //Get size of collider
        block= gameObject.GetComponent<Rigidbody>();
        halfColliderWidth = GetComponent<BoxCollider>().size.x / 2;
        halfColliderHight = GetComponent<BoxCollider>().size.y / 2;

    }
    public bool PushTo(float d,Vector3 v){
        bool moved=false;
        //Snap vector to cardinal direction
        Vector3 direction=v;


        //Test for collision in next movement
        Collider[] colobj=Physics.OverlapBox(
          block.position+d*direction, (transform.localScale/2)-
          new Vector3(microdist,microdist,microdist)
        );
        if(colobj.Length == 1){
            bool succes=false;
            if(colobj[0].tag=="Box"){
                Push pushblock =colobj[0].gameObject.GetComponent<Push>();
                succes=pushblock.PushTo(d,direction);
            }
            if (succes==true){
                block.MovePosition(block.position +(d*direction));
                moved=true;
            }
            
        }else if(colobj.Length == 0){
            block.MovePosition(block.position +(d*direction));
            moved=true;
        }
        return moved;
    }
}
