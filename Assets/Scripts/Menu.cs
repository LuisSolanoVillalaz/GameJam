using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject options;
    public GameObject credito;
    public GameObject menu;
    bool opt = false;
    bool cre = false;
    public void jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void opciones()
    {
        if(opt)
        {
            options.SetActive(false);
            menu.SetActive(true);
            opt = false;
        }
        else
        {
            opt= true;
            options.SetActive(true);
            menu.SetActive(false);
        }
        
    }

    public void creditos()
    {
        if (cre)
        {
            options.SetActive(false);
            menu.SetActive(true);
            credito.SetActive(false);
            cre = false;
        }
        else
        {
            cre= true;
            options.SetActive(false);
            menu.SetActive(false);
            credito.SetActive(true);
        }

    }
    public void salir()
    {
        Application.Quit();
    }
}
