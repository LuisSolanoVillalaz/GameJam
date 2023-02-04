using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPLIT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player2D>().tplit= true;
            other.GetComponent<Player2D>().tplat = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<Player2D>().tplit = false;
        }
    }
}
