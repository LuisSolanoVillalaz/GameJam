using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    bool isfloating = false;
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
        else if (!cam.cen ) { }
        {

            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                rb.velocity = new UnityEngine.Vector3 (-Input.GetAxisRaw("Horizontal") * 5,0, 0 );
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
        if (Input.GetAxisRaw("Hold") != 0 && collision.gameObject.tag=="Box" && pullBox==null){
            pullBox=collision.rigidbody;
            boxPos=pullBox.position-rb.position;
       }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (Input.GetAxisRaw("Hold") != 0 && collision.gameObject.tag=="Box" && pullBox==null){
            pullBox=collision.rigidbody;
            boxPos=pullBox.position-rb.position;
       }
    }
}
