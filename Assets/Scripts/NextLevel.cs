using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public GameObject music;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                DontDestroyOnLoad(music);

                var musicEmitter = music.GetComponent<FMODUnity.StudioEventEmitter>();
                musicEmitter.SetParameter("location", 1);

            }


            var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
            emitter.Play();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);     //next scene
        }
    }
}
