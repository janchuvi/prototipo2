using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteShooter : MonoBehaviour {

    public Pool pool;
    float x;
    float y;
    float z;
    Vector3 pos;
    bool caer = true;
    // Update is called once per frame
    private void Start()
    {
      
    }
    void Update () {
        if(caer == true)
        Fall();
	}
    void Fall()
    {
        caer =false;
        x = Random.Range(-75f, 120f);
        y = 176.8f;
        z = Random.Range(120f, 200f);
        pos = new Vector3(x, y, z);
        GameObject go = pool.Get();
        go.transform.position = pos;
        StartCoroutine(meteoriteWait());
    }
    IEnumerator meteoriteWait()
    {
        yield return new WaitForSeconds(Random.Range(10,21));
        caer = true;
    }
}
