using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustStormScript : MonoBehaviour {

    float speed = 10f;
    Vector3 iniPos;
    Vector3 farPos;
    bool right;
    bool left;
    int randoNum;
    void Start () {
        iniPos = this.transform.position;
        right = true;
        left = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(right == true)
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        else if(left == true) transform.Translate(Vector3.right * Time.deltaTime * -speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "SandCol")
        {
            farPos = this.transform.position;
           
            StartCoroutine(ChangeSides());
            randoNum = Random.Range(0, 2);
            Debug.Log(randoNum+""+"random");
        }
    }
    IEnumerator ChangeSides()
    {
        right = false;
        yield return new WaitForSeconds(5);
        if(randoNum == 0)
        {
            left = true;
            this.transform.position = farPos;
        }
        else if(randoNum == 1)
        {
            right = true;
            left = false;
            this.transform.position = iniPos;
        }
    
    }
}
