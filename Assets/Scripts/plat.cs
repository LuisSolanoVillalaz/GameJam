using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat : MonoBehaviour
{
    public BoxCollider padre;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (other.GetComponent<Rigidbody>().velocity.y >= 0)
            {
                padre.enabled=false;
            }
            else
            {
                padre.enabled= true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
                padre.enabled = true;

        }
    }
}
