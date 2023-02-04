using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Move : MonoBehaviour
{   

    //Variables for collider calculations
    Rigidbody block;
    float halfColliderWidth;
    float halfColliderHight;
    
    //Variables to store input
    Vector3 Pos;
    
    //variable for conditional
    bool enable=false;
    public float traveldistance=8f;
    float timer=0;

    //Tolerance for collider detection
    float microdist=0.01f;

    
    // Start is called before the first frame update
    void Start()
    {
        //Get size of collider
        block= gameObject.GetComponent<Rigidbody>();
        halfColliderWidth = GetComponent<BoxCollider>().size.x / 2;
        halfColliderHight = GetComponent<BoxCollider>().size.y / 2;

    }
   
    private void FixedUpdate() {
        //If enable run move function
            Pos.x=Input.GetAxisRaw("Horizontal");
            Pos.z=Input.GetAxisRaw("Vertical");
            if(timer>=0.2 && (Pos.x!=0 ||Pos.z!=0)){
                MoveTo(Pos);
                timer=0;
            }
            if(timer<=0.2){
                timer=timer+Time.deltaTime;
            }

    }


    public void MoveTo(Vector3 v){
        Vector3 direction=v;
        Collider[] colobj=Physics.OverlapBox(
          block.position+traveldistance*direction, (transform.localScale/2)-
          new Vector3(microdist,microdist,microdist)
        );
        if(colobj.Length == 1){
            bool succes=false;
            if(colobj[0].tag=="Box"){
                Push pushblock =colobj[0].gameObject.GetComponent<Push>();
                succes=pushblock.PushTo(traveldistance,direction);
            }
            if(succes==true){
                block.MovePosition(block.position +(traveldistance*direction));
            }
            
        }else if(colobj.Length == 0){
            block.MovePosition(block.position +(traveldistance*direction));
        }

    }


}
