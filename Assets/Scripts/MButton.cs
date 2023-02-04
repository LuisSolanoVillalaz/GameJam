using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MButton : MonoBehaviour
    
{
    Vector3 firstpos;
    int num = 0;
    public float pressedbutton = 0.5f;
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
            num++;
           pressed= true;
            objective.transform.GetComponent<Renderer>().material = active;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player" || other.tag == "Caja")
        {
            num--;
            

        }
    }
    private void Update()
    {
        if (pressed && button.transform.position.y >= firstpos.y-0.5f && num>0) {
            button.transform.position = button.transform.position - new Vector3(0, pressedbutton * Time.fixedDeltaTime, 0);

        }
        else if(firstpos.y > button.transform.position.y&& num <=0)
        {
            pressed = false;
            objective.transform.GetComponent<Renderer>().material = unactive;
            button.transform.position = button.transform.position + new Vector3(0, pressedbutton * Time.fixedDeltaTime, 0);

        }

    }

}
