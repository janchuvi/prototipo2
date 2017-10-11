using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour {

    // Use this for initialization
    public Transform target;
    GameObject arrow;
  public Player jugador;
    public DataController controller;

    private void Start()
    {

        controller = GameObject.Find("DataController").GetComponent<DataController>();
        jugador = GetComponentInParent<Player>();
       

        arrow = GameObject.Find("Arrow");
        arrow.SetActive(false);
    }
    void Update()
    {
      
        transform.LookAt(target);
    }
    private void OnTriggerStay(Collider other)
    {
        
         if (other.name == "FlechaModulo")
        {
          
            if (jugador.objetivos[0] == false)
            {
                target = GameObject.Find("MóduloLunarDelApolo11").GetComponent<Transform>();
                arrow.SetActive(true);
            }
            else
            {
                arrow.SetActive(false);
            }
        }
        else if (other.name == "FlechaEmu")
        {
            if (jugador.objetivos[1] == false)
            {
                target = GameObject.Find("EMU").GetComponent<Transform>();
                arrow.SetActive(true);
            }
            else
            {
                arrow.SetActive(false);
            }
        }
      else  if (other.name == "FlechaAristarco")
        {
            Debug.Log("flechacrater");
            if (jugador.objetivos[2] == false)
            {
                arrow.SetActive(true);
                target = GameObject.Find("Aristarco").GetComponent<Transform>();

            }
            else
            {
                arrow.SetActive(false);
            }
        }
        if (other.name == "FlechaRover")
        {
            if (jugador.objetivos[3] == false)
            {
                target = GameObject.Find("roverover").GetComponent<Transform>();
                arrow.SetActive(true);
            }
            else
            {
                arrow.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        target = null;
        arrow.SetActive(false);
    }
}
