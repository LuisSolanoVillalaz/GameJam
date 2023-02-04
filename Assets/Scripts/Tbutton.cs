using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tbutton : MonoBehaviour
    
{
    Vector3 firstpos;
    public GameObject button;
    public GameObject objective;
    public Material active;
    public Material unactive;
    public bool pressed;
    private void Start()
    {
        firstpos= button.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Caja")
        {
           pressed= true;
            objective.transform.GetComponent<Renderer>().material = active;
            
        }
    }
    private void Update()
    {
        if (pressed && button.transform.position.y >= firstpos.y-0.5f) {
            button.transform.position = button.transform.position - new Vector3(0, 0.5f * Time.deltaTime, 0);

        }
        
    }

}
