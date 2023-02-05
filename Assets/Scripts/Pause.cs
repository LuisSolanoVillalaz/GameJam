using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject options;
    public GameObject pause;
    bool paused = false;
    bool opt = false;
    public void salir()
    {
        SceneManager.LoadScene(0);
    }
    public void pausar() {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
            pause.SetActive(false);
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
            pause.SetActive(true);
           
        }
    
    }
    public void opciones()
    {
        if(opt)
        {
            options.SetActive(false);
            pause.SetActive(true);
            opt = false;
        }
        else
        {
            opt= true;
            options.SetActive(true);
            pause.SetActive(false);
        }
        
    }
}
