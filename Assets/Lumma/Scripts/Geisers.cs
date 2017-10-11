using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geisers : MonoBehaviour {
    ParticleSystem particle;
    int i;
    bool caer= true;
	// Use this for initialization
	void Start () {
        particle = this.gameObject.GetComponent<ParticleSystem>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (caer == true)
            Geiseres();
    }
    void Geiseres()
    {
        caer = false;
        BoxCollider collider = this.GetComponent<BoxCollider>(); ;
        i = Random.Range(0, 21);
        //    Debug.Log(i);
        var main = particle.main;
        if (i >= 0 && i <= 10)
        {
            main.startLifetime = 5.4f;
            collider.enabled = true;
        }
        if (i >= 11 && i <= 20) { 
            main.startLifetime = 0;
        collider.enabled = false;
    }
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(Random.Range(10, 21));
        caer = true;
    }
}
