using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public bool open;
    public Vector3 difp = new Vector3(1.2f, 0, -3.5f);
    public Quaternion difr = new Quaternion(0, 0, 0,0);
    public float movespeed = 1f;
    public float rotspeed = 1f;
    Vector3 firstpos;
    Quaternion firstRot;
    void Start()
    {
        firstpos= transform.position;
        firstRot= transform.rotation;   
    }

    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, firstpos + difp, movespeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, firstRot*difr, rotspeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, firstRot, rotspeed * Time.fixedDeltaTime);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, firstpos, movespeed * Time.fixedDeltaTime);
           
        }
    }
    
}
