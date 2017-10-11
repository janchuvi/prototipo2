using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rover : MonoBehaviour {
     float speed = 2f;
    float avanzar;
    Vector3 iniPos;
    private void Start()
    {
        iniPos = this.transform.position;
        avanzar = 0;

    }
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        avanzar += Time.deltaTime;
       /// Debug.Log(avanzar);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "RoverColl")
        {
            transform.position = new Vector3 (iniPos.x,iniPos.y,iniPos.z);
        }
    }
}
