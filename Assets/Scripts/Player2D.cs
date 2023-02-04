using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    bool isfloating = false;
    public bool tplat = false;
    public bool tplit = false;
    bool canpause=true;
    public Rigidbody rb;
    movCam cam;
    bool changing;
    public GameObject menu;

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
            Invoke("unpause", 1f);
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
            if (Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0)
            {
                rb.velocity=new UnityEngine.Vector3 (-Input.GetAxis("Vertical")*5,rb.velocity.y, Input.GetAxisRaw("Horizontal") * 5);
            }
            
        }
        else if (!cam.cen ) { }
        {

            if (Input.GetAxis("Horizontal") != 0)
            {
                rb.velocity = new UnityEngine.Vector3 (0, 0, Input.GetAxisRaw("Horizontal") * 5);
            }
        }
       
       ///////////////////////////////////////////////////////////////////////////////
        
    }

    public void unpause()
    {
        canpause = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
