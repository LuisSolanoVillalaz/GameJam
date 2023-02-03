using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{
    public delegate void HistoryListener(List<GameObject> b, Vector2 d);
    public static event HistoryListener HL;
    public static void agregarHistoria(List<GameObject> b, Vector2 d){
        HL(b,d);
    }

    public delegate void Ganar(List<GameObject> b, Vector2 d);
    public static event Ganar Ga;
    public static void agregarGanar(List<GameObject> b, Vector2 d){
        Ga(b,d);
    }
   
}
