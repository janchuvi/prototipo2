using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautJump : MonoBehaviour {

    public ParticleSystem particle;
    Rigidbody rb;
    bool yes=true;
    void Start()
    {
       
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (yes==true)
        {
            subir();
        }
    }
    private void OnDestroy()
    {
        particle = null;
        rb = null;
      
    }
    void subir()
    {
        yes = false;
        rb.AddForce(transform.forward * 10, ForceMode.Impulse);
        rb.AddForce(transform.up * 2, ForceMode.Impulse);
        if (transform.localPosition.z <= 276)
            this.transform.eulerAngles = new Vector3(-90f, 180f, 0f);
        if(transform.localPosition.z>=379)
            this.transform.eulerAngles = new Vector3(-90f, 0f, 0f);
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        yes = true;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        var main = particle.main;
        if(collision.gameObject.name == "TerrainMoon")
        {
            main.startLifetime = 16f;
            main.startSpeed = 5.5f;
            Debug.Log("ground");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        var main = particle.main;
        if (collision.gameObject.name == "TerrainMoon")
        {
            main.startLifetime = 0;
            main.startSpeed = 0;
            Debug.Log("ground");
           
        }
    }
}
