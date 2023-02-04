using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movCam : MonoBehaviour
{
    public GameObject padre;
    RaycastHit b;
    public bool cen = false;
    public bool canmove = true;
    public Material tp1;

    public int movespeed = 7;
    public int rotspeed = 70;


    public Vector3 Pos1 = new Vector3(0, 20, 0);
    public Quaternion Rot1 = new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f);

    public Vector3 Pos2 = new Vector3(20, 1, 0);
    public Quaternion Rot2 = new Quaternion(0, -0.7f, 0, 0.7f);
    
    public void transicion()
    {
        Debug.DrawRay(padre.transform.position, -Vector3.right, Color.yellow);
        if (cen && canmove)
        {
            if (Physics.Raycast(padre.transform.position, -Vector3.up, out b))
            {

                padre.GetComponent<Rigidbody>().useGravity = true;
                padre.transform.position = new Vector3(padre.transform.position.x, b.transform.position.y + 1, padre.transform.position.z);
                GameObject[] cajas = GameObject.FindGameObjectsWithTag("Caja");
                for (int i = 0; i < cajas.Length; i++)
                {
                    if (Physics.Raycast(cajas[i].transform.position, -Vector3.up, out b))
                    {
                        cajas[i].GetComponent<Rigidbody>().useGravity = true;
                        cajas[i].transform.position = new Vector3(cajas[i].transform.position.x, b.transform.position.y + 1, cajas[i].transform.position.z);
                    }
                }
            }

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Pos2, movespeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Rot2, rotspeed * Time.fixedDeltaTime);
          
            if (transform.localPosition == Pos2 && transform.rotation == Rot2) {
                if (padre.GetComponent<Player2D>().tplat) {

                   
                    if (Physics.Raycast(padre.transform.position, -Vector3.right, out b))
                    {
                      

                        padre.transform.position = new Vector3(b.point.x + 1, padre.transform.position.y, padre.transform.position.z);
                        padre.GetComponent<Player2D>().tplat = false;

                    }
                }
                else if (padre.GetComponent<Player2D>().tplit)
                {

                    if (Physics.Raycast(padre.transform.position, Vector3.right, out b))
                    {


                        padre.transform.position = new Vector3(b.point.x - 1, padre.transform.position.y, padre.transform.position.z);
                        padre.GetComponent<Player2D>().tplit= false;

                    }
                }
                canmove = false;
                cen = false;
                Invoke("cant", 1.5f);
            }

        }
        else if (!cen && canmove)
        {

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Pos1, movespeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Rot1, rotspeed * Time.deltaTime);
            
            if (transform.localPosition == Pos1 && transform.rotation == Rot1)
            {
            
                if (Physics.Raycast(padre.transform.position, -Vector3.up, out b))
                {

                    String baseMaterialName = tp1.name; 
                    String assignedMaterialName = b.transform.GetComponent<Renderer>().sharedMaterial.name;

                    if (assignedMaterialName.Contains(baseMaterialName)){
                        padre.GetComponent<Rigidbody>().useGravity = false;
                        padre.transform.position = padre.transform.position + new Vector3(0, 10, 0);
                        GameObject[] cajas = GameObject.FindGameObjectsWithTag("Caja");
                        for (int i = 0; i < cajas.Length; i++)
                        {
                            cajas[i].GetComponent<Rigidbody>().useGravity=false;
                            cajas[i].transform.position= cajas[i].transform.position + new Vector3(0, 10, 0);
                        }
                    }
                }
                canmove = false;
                cen = true; 

               

              
                Invoke("cant", 1.5f);
            }
        }


    }
    private void Update()
    {
    if(cen)
        {

            if (Physics.Raycast(padre.transform.position, -Vector3.up, out b))
            {

                String baseMaterialName = tp1.name;
                String assignedMaterialName = b.transform.GetComponent<Renderer>().sharedMaterial.name;

                if (!assignedMaterialName.Contains(baseMaterialName))
                {
                    padre.GetComponent<Rigidbody>().useGravity = true;
                    GameObject[] cajas = GameObject.FindGameObjectsWithTag("Caja");
                    for (int i = 0; i < cajas.Length; i++)
                    {
                        if (Physics.Raycast(cajas[i].transform.position, -Vector3.up, out b))
                        {
                            cajas[i].GetComponent<Rigidbody>().useGravity = true;
                   
                        }
                    }
                }
            }
        }
    }

    void cant()
    {
        canmove =true;
    }
}
