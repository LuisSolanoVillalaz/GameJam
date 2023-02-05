using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    public bool isfloating = false;
    UnityEngine.Vector3 underbox;
    UnityEngine.Vector3 width;
    public bool tplat = false;
    public bool tplit = false;

    public bool canpause=true;
    public Rigidbody rb;

    movCam cam;
    bool changing;
    public GameObject menu;
    Rigidbody pullBox;
    UnityEngine.Vector3 boxPos;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<movCam>();
        underbox=  new UnityEngine.Vector3 (0f,-0.25f,0f);
        width= new UnityEngine.Vector3(0.2f,0.03f,0.2f);
    }
    private void Update()
    {
        if (Input.GetAxis("Fire1") != 0 && cam.canmove)
        {
            changing= true;
            canpause = false;
         

        }
        if (Input.GetAxis("Pause")!=0 && canpause)
        {
            canpause = false;
            menu.GetComponent<Pause>().pausar();
            float timeRemaining = 1;
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                canpause = true;
            }
        }
        
    }
    private void FixedUpdate()
    {
        
        if(Physics.OverlapBox(rb.position+underbox, width).Length>1){
            isfloating=false;
            rb.velocity= UnityEngine.Vector3.zero;
        }else{
            isfloating=true;
            
        }
        
        if(changing && cam.canmove && !isfloating )
        {
            cam.transicion();
           
        }
        else
        {
            changing= false;
        }

        /////////////// ESTO ES MOVIMIENTO PROVISIONAL//////////////////////////////////////7
        if (cam.cen )
        {
            if (Input.GetAxisRaw("Horizontal") != 0|| Input.GetAxisRaw("Vertical") != 0)
            {
                rb.velocity=new UnityEngine.Vector3 (-Input.GetAxisRaw("Horizontal") * 5, rb.velocity.y,(-Input.GetAxisRaw("Vertical")*5));      
            }
            
        }
        else if (!cam.cen ) {

            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                rb.velocity = new UnityEngine.Vector3 (-Input.GetAxisRaw("Horizontal") * 5,rb.velocity.y, 0 );
                
            }
            if (!isfloating && Input.GetAxisRaw("Jump") != 0 && pullBox == null)
            {
                rb.velocity = new UnityEngine.Vector3(rb.velocity.x, 6, rb.velocity.z);
            }
        }

       if (Input.GetAxisRaw("Hold") == 0 && pullBox!=null){
        pullBox=null;
       }else if(pullBox!=null){
                    pullBox.position = rb.position+boxPos;
                }
       
       ///////////////////////////////////////////////////////////////////////////////
        
    }

    public void unpause()
    {
        canpause = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetAxisRaw("Hold") != 0 && collision.gameObject.tag=="Box" && pullBox==null && !isfloating && rb.position.y<collision.rigidbody.position.y){
            pullBox=collision.rigidbody;
            boxPos=pullBox.position-rb.position;
       }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (Input.GetAxisRaw("Hold") != 0 && collision.gameObject.tag=="Box" && pullBox==null && !isfloating &&rb.position.y<collision.rigidbody.position.y){
            pullBox=collision.rigidbody;
            boxPos=pullBox.position-rb.position;
       }
    }
     void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(rb.position+underbox, width*2);
    }
}
