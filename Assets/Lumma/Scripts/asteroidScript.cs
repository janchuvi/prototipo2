using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour {

    ParticleSystem particle;
  // public GameObject explosion;
    private Pool pool;
	void Start () {
        particle = GameObject.Find("colaAsteroide").GetComponent<ParticleSystem>();
        pool = GameObject.Find("MeteoritesPool").GetComponent<Pool>();
       // explosion = GameObject.Find("FireExplosion");
      //  explosion.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * 10);
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "TerrainMars")
        {
           // explosion.SetActive(true);
            pool.Return(this.gameObject);
        }
    }
}
