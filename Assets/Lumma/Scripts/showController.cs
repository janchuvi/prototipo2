using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showController : MonoBehaviour {
    public GameObject[] stuff;
    public DataController controller;
    // Use this for initialization
    void Start()
    {
        controller = GameObject.Find("DataController").GetComponent<DataController>();

        stuff = new GameObject[3];
        //  
        stuff[0] = GameObject.Find("Player1");
        stuff[1] = GameObject.Find("Player2");
        stuff[2] = GameObject.Find("Player3");
       
        for (int i = 0; i < stuff.Length; i++)
        {
            stuff[i].SetActive(false);

        }
        if (controller.characterSelected.name == "Player1")
        {
            stuff[0].SetActive(true);
        }
        else if (controller.characterSelected.name == "Player2")
        {
            stuff[1].SetActive(true);
        }
        else if (controller.characterSelected.name == "Player3")
        {
            stuff[2].SetActive(true);
        }
    }
        // Update is called once per frame
        void Update () {
		
	}
}
