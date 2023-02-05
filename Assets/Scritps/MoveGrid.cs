using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MoveGrid : MonoBehaviour
{   

    //Variables for collider calculations
    Rigidbody block;
    float halfColliderWidth;
    float halfColliderHight;
    
    //Variables to store input
    Vector3 Pos;
    Vector3 direc;
    Vector3 pd;
    //variable for conditional

    public float traveldistance=1f;
    public float jumpforce=4f;
    float timer=0;
    public float speed=0.2f;
    public float movetime=0.01f;
    bool isMoving=false;
    public bool isGrounded=true;
    int count=1;
   
    //Tolerance for collider detection
    float microdist=0.1f;

    
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
            
            if(timer>=movetime && !isMoving && isGrounded && Input.GetButton("Jump")){
                block.AddForce(transform.up*jumpforce, ForceMode.Impulse); 
            }
            if(timer>=movetime && (Pos.x!=0 ||Pos.z!=0) && !isMoving &&count==1){
                direc=block.position+traveldistance*Pos;
                MoveTo(Pos);
                
                timer=0;
            }
            if(timer<=movetime){
                timer=timer+Time.deltaTime;
            }
            
            if(isMoving){
                count=0;
                float vel=block.velocity.y;
                block.MovePosition( Vector3.MoveTowards(block.position,direc,speed));
                block.velocity=new Vector3(0,vel,0);
                if(block.position.x==direc.x&&block.position.z==direc.z){
                    isMoving=false;
                }
            }

            if(Physics.Raycast(block.position,Vector3.down,halfColliderHight)){
                isGrounded=true;
                count=1;
            }else{
                isGrounded=false;
            }
            

    }


    public void MoveTo(Vector3 v){
        Vector3 direction=v;
        Collider[] colobj=Physics.OverlapBox(
            block.position+traveldistance*direction, (transform.localScale/2)-
            new Vector3(microdist*2,microdist*5,microdist*2)
        );
        pd=direction;
       
        if(colobj.Length >= 1 && isGrounded){
            
            bool succes=false;
            if(colobj[0].tag=="Box"){
                Push pushblock =colobj[0].gameObject.GetComponent<Push>();
                succes=pushblock.PushTo(traveldistance,direction);
            }
            if(succes==true){
                isMoving=true;
            }
            
        }else if(colobj.Length == 0){
            
            if(Input.GetButton("Hold")&&isGrounded){
                Collider[] pullobj=Physics.OverlapBox(
                    block.position-traveldistance*direction, (transform.localScale/2)-new Vector3(microdist,microdist,microdist)
                );
                if(pullobj.Length == 1){
                    bool succes=false;
                    if(pullobj[0].tag=="Box"){
                        Push pushblock =pullobj[0].gameObject.GetComponent<Push>();
                        succes=pushblock.PushTo(traveldistance,direction);
                    }
                    if(succes==true){
                        isMoving=true;
                    }
                }else{
                    isMoving=true;
                }
            }else{
                isMoving=true;
            }
        }

    }


}
