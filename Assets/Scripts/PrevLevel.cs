using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrevLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
            emitter.Play();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);     //prev scene
        }
    }
}
