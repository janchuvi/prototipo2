using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CassiniMovement : MonoBehaviour {
    public NavMeshAgent nma;

    public int puntoActual;
    public Transform[] punto;
	// Use this for initialization
	void Start () {
        nma = GetComponent <NavMeshAgent>();
        puntoActual = 0;
        nma.SetDestination(punto[puntoActual].position);

	}
	
	// Update is called once per frame
	void Update () {
        if (!nma.pathPending && nma.hasPath && nma.remainingDistance < 0.5f)
        {
            puntoActual++;
            if (puntoActual == (punto.Length))
            {
                puntoActual = 0;

            }
            Transform destino = punto[puntoActual];
            nma.SetDestination(destino.position);
        }
	}
}
