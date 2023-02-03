using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Snaper : MonoBehaviour
{   //Visible in editor
    public bool CanPush=true;//can push blocks
    public bool CanBePush=true;//can be push by other block
    public bool MoveX=true;//can move in X axis
    public bool MoveY=true;//can move in Y axis

    //Variables for collider calculations
    Rigidbody block;
    float halfColliderWidth;
    float halfColliderHight;
    
    //Variables to calculate mouse relative distance to block

    Vector3 Pos;
    public float timer=0f;
    
    //variable for conditional
    bool enable=false;
    float traveldistance=1f;
    
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
            Pos.x=Input.GetAxis("Horizontal");
            Pos.z=Input.GetAxis("Vertical");
            if(timer>=0.5){
                 MoveTo(Pos);
                 timer=0;
            }
           timer=timer+Time.deltaTime;


    }



    
    public void MoveTo(Vector3 v){
        List<GameObject> objMoved = new List<GameObject>();
        Snaper pushblock=null;
        //Snap vector to cardinal direction
        Vector3 direction=new Vector3(0,0,0);
        if(v.x<0){
            direction=new Vector3(-1,0,0);
        }else if(v.x>0){
            direction=new Vector3(1,0,0);
        }else if(v.z<0){
            direction=new Vector3(0,0,-1);
        }else if(v.z>0){
            direction=new Vector3(0,0,1);
        }
        //Make vector (1,1) for collider calculation
        Vector3 collTest= direction.x!=0?new Vector3(direction.x,1,1):new Vector3(1,1,direction.z);
        //Test if valid movement
        if((direction.x!=0&&!MoveX)||(direction.z!=0&&!MoveY)){
            enable=false;
            return;
            }
            
        //Test for collision in next movement
        Collider[] colobj=Physics.OverlapBox(
          block.position+direction, (transform.localScale/2)-
          new Vector3(microdist,microdist,microdist)
                                        );
        /*if(colobj.Length != 0){
            Debug.Log(colobj);
             pushblock =colobj[0].gameObject.GetComponent<Snaper>();
        }
        if(pushblock!=null&&CanPush){
           objMoved= pushblock.PushTo(direction);
        }*/
        if(colobj.Length == 0||objMoved.Count>0){
            block.MovePosition(block.position +(traveldistance*direction));
            objMoved.Add(gameObject);
            //EventManager.agregarHistoria(objMoved,direction);
        }
        enable=false;  
       
        /*/
        //test what stops the movement
        Debug.Log("\n\n\n");
        Debug.Log(direction.x+" "+direction.z);
        Debug.Log(collTest.x+" "+collTest.z);
        
        //Debug.DrawLine(block.position+new Vector3(((halfColliderWidth+microdist)*collTest.x),(halfColliderHight-microdist)*collTest.y),
        //block.position+new Vector3(((halfColliderWidth-microdist+(traveldistance*Mathf.Abs(direction.x)))*collTest.x),(-halfColliderHight+microdist-(traveldistance*Mathf.Abs(direction.y)))*collTest.y));
        Debug.DrawLine(block.position,block.position+Vector3.down);
        Debug.DrawLine(
            block.position+new Vector3((halfColliderWidth+(microdist*(direction.z==0?1:-1)))*collTest.x,-halfColliderHight,
                                       (halfColliderWidth+(microdist*(direction.x==0?1:-1)))*collTest.z),
            block.position+new Vector3((((halfColliderWidth-microdist)*(direction.z==0?1:-1))+(traveldistance*Mathf.Abs(direction.x)))*collTest.x,halfColliderHight,
                                       (((halfColliderWidth-microdist)*(direction.x==0?1:-1))+(traveldistance*Mathf.Abs(direction.z)))*collTest.z )
       ,Color.red                     );
        //*/
    }

    public List<GameObject> PushTo(Vector3 v, bool overpush = false){
        List<GameObject> objMoved = new List<GameObject>();
        Snaper pushblock=null;
        //Snap vector to cardinal direction
        Vector3 direction= v;
        //Make vector (1,1) for collider calculation
        Vector3 collTest= direction.x!=0?new Vector3(direction.x,1):new Vector3(1,direction.y);
        if(!CanBePush){
            return objMoved;
            }
        //Test for collision in next movement
        Collider2D colobj=Physics2D.OverlapArea(
            block.position+new Vector3((halfColliderWidth+(microdist*(direction.y==0?1:-1)))*collTest.x,
                                       (halfColliderHight+(microdist*(direction.x==0?1:-1)))*collTest.y),
            block.position+new Vector3((((halfColliderWidth-microdist)*(direction.y==0?1:-1))+(traveldistance*Mathf.Abs(direction.x)))*collTest.x,
                                       (((halfColliderHight-microdist)*(direction.x==0?1:-1))+(traveldistance*Mathf.Abs(direction.y)))*collTest.y )
        );
        if(colobj!= null){
             pushblock =colobj.gameObject.GetComponent<Snaper>();
        }
        if(pushblock!=null&&CanPush){
           objMoved= pushblock.PushTo(direction);
        }
        if(colobj==null||objMoved.Count>0||overpush){
            block.MovePosition(block.position +(traveldistance*direction));
            objMoved.Add(gameObject);
        }
        
        /*
        //test what stops the movement
        Debug.Log("\n\n\n");
        Debug.Log(direction.x+" "+direction.z);
        Debug.Log(collTest.x+" "+collTest.z);
        //Debug.DrawLine(block.position+new Vector3(((halfColliderWidth+microdist)*collTest.x),(halfColliderHight-microdist)*collTest.y),
        //block.position+new Vector3(((halfColliderWidth-microdist+(traveldistance*Mathf.Abs(direction.x)))*collTest.x),(-halfColliderHight+microdist-(traveldistance*Mathf.Abs(direction.y)))*collTest.y));
        Debug.DrawLine(
           block.position+new Vector3((halfColliderWidth+(microdist*(direction.z==0?1:-1)))*collTest.x,0,
                                       (halfColliderHight+(microdist*(direction.x==0?1:-1)))*collTest.z),
            block.position+new Vector3((((halfColliderWidth-microdist)*(direction.z==0?1:-1))+(traveldistance*Mathf.Abs(direction.x)))*collTest.x,0,
                                       (((halfColliderHight-microdist)*(direction.x==0?1:-1))+(traveldistance*Mathf.Abs(direction.z)))*collTest.z )
       ,Color.red                     );
        //*/

        return objMoved;
    }

}
