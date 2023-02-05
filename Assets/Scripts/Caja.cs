using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{
    Rigidbody rb;
    Vector3 v = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        v = Vector3.zero;
    }
    void Update()
    {
        if(rb.velocity.x + rb.velocity.z > 0 && rb.velocity.y == 0 && (v.x + v.z) == 0)
        {
            //start
        }
        else if (rb.velocity.x+rb.velocity.z == 0 && v.x+v.z>0 || rb.velocity.y != 0)
        {
            //stop
        } 

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "ground") { 
        //suelo
        }
    }
}
