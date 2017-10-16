using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterControllerScript : MonoBehaviour {

	public GameObject moon;
	public GameObject encelado;

	public GameCamera gameCamera;
    public GameObject[] stuff;
   public  DataController controller;
    public Transform male;
    public GameObject[] legs;
  //  public GameObject waist;
    public Material[] sky;
    public GameObject[] objetives;
    public Player player;
 //   Animation anim;

    IEnumerator ObjectiveMoon()
    {
        objetives[0].SetActive(true);
        yield return new WaitForSeconds(2);
        objetives[0].SetActive(false);
    }
    IEnumerator ObjectiveEnceladus()
    {
        objetives[1].SetActive(true);
        yield return new WaitForSeconds(2);
        objetives[1].SetActive(false);
    }

    void Start() {
		moon.SetActive (false);
		encelado.SetActive (false);

        controller = GameObject.Find("DataController").GetComponent<DataController>();
        objetives[0].SetActive(false);
        objetives[1].SetActive(false);
        stuff = new GameObject[8];
        //  
        stuff[0] = GameObject.Find("Player1");
        stuff[1] = GameObject.Find("Player2");
       // stuff[2] = GameObject.Find("TerrainMars");
     //   stuff[3] = GameObject.Find("TerrainMoon");
      //  stuff[4] = GameObject.Find("TerrainEncelado");
        stuff[5] = GameObject.Find("PlayerScateboard");
        stuff[6] = GameObject.Find("GameObjectRover");
        stuff[7] = GameObject.Find("Player3");

       
        stuff[0].SetActive(false);
        stuff[1].SetActive(false);
       // stuff[2].SetActive(false);
      //  stuff[3].SetActive(false);
      //  stuff[4].SetActive(false);
       // stuff[5].SetActive(false);
        //stuff[6].SetActive(false);
        stuff[7].SetActive(false);
        //     for (int i = 0; i < stuff.Length; i++){
        //       stuff[i].SetActive(false);

        // }

        if (controller.characterSelected.name == "Player1")
        {
            stuff[0].SetActive(true);
            legs = new GameObject[4];
            legs[0] = GameObject.Find("Base HumanRThigh");
            legs[1] = GameObject.Find("Base HumanLThigh");
            legs[2] = GameObject.Find("Base HumanRCalf");
            legs[3] = GameObject.Find("Base HumanLCalf");
            male = GameObject.Find("PreBase").transform.GetChild(0);
            //   male = GameObject.Find("Male");
            stuff[5] = GameObject.Find("PlayerScateboard");
            stuff[6] = GameObject.Find("GameObjectRover");
            player = stuff[0].GetComponent<Player>();
        }
        else if (controller.characterSelected.name == "Player2")
        {
            stuff[1].SetActive(true);
            legs = new GameObject[4];
            player = stuff[1].GetComponent<Player>();
            male = GameObject.Find("PreBase").transform.GetChild(0);
          //  male = GameObject.Find("Male");
            stuff[5] = GameObject.Find("PlayerScateboard");
            stuff[6] = GameObject.Find("GameObjectRover");
            legs[0] = GameObject.Find("Base AlienRThigh");
            legs[1] = GameObject.Find("Base AlienLThigh");
            legs[2] = GameObject.Find("Base AlienRCalf");
            legs[3] = GameObject.Find("Base AlienLCalf");
            //  anim = stuff[1].GetComponent<Animation>();
        }
        else if (controller.characterSelected.name == "Player3")
        {
            stuff[7].SetActive(true);
            player = stuff[7].GetComponent<Player>();
            legs = new GameObject[4];
            legs[0] = GameObject.Find("Base HumanRThigh");
            legs[1] = GameObject.Find("Base HumanLThigh");
            legs[2] = GameObject.Find("Base HumanRCalf");
            legs[3] = GameObject.Find("Base HumanLCalf");
            male = GameObject.Find("PreBase").transform.GetChild(0);
            //    male = GameObject.Find("Male");
            stuff[5] = GameObject.Find("PlayerScateboard");
            stuff[6] = GameObject.Find("GameObjectRover");
        }
        if (controller.planetSelected.name == "Terrain1")
        {//mars
        //    stuff[2].SetActive(true);
           
        }
        else if(controller.planetSelected.name == "Terrain2")
        {//moon

            RenderSettings.skybox = sky[0];
			moon.SetActive (true);
         //     stuff[3].SetActive(true);
         //   stuff[6].SetActive(true);
         //   stuff[5].SetActive(false);
            StartCoroutine(ObjectiveMoon());
            Physics.gravity = new Vector3(0, -100.0F, 0);

        }
        else if (controller.planetSelected.name == "Terrain3")
        {//enceladus
         //   anim.Play();
            RenderSettings.skybox = sky[1];
            //  waist.transform.eulerAngles = new Vector3(0,90,0);
            player.turbine[0] = GameObject.Find("Turbine");
            player.turbine[1] = GameObject.Find("Turbine2");
         //   stuff[4].SetActive(true);
            stuff[5].SetActive(true);
            stuff[6].SetActive(false);
            legs[0].transform.eulerAngles = new Vector3(0, 0, 90);
            legs[1].transform.eulerAngles = new Vector3(0, 0, 90);
            legs[0].transform.localRotation = Quaternion.Euler(60, -200f, -20);
            legs[0].transform.localPosition = new Vector3(-0.01762551f, -0.02998248f, -0.0442522f);
            legs[1].transform.localRotation = Quaternion.Euler(60, -200f, -20);
            legs[1].transform.localPosition = new Vector3(-0.01738821f, 0.1254879f, 0.04425144f);
            legs[2].transform.localRotation =  Quaternion.Euler(0.058f, -1.198f, 6);
            legs[3].transform.localRotation =  Quaternion.Euler(0.058f, -1.198f, 6);
            if (controller.characterSelected.name == "Player3")
            {
                male.transform.localPosition = new Vector3(0, 0.15f, 0f);
            }else if (controller.characterSelected.name == "Player1")
            {
                male.transform.localPosition = new Vector3(0.1f, 0.67f, 0f);
            }
            else if (controller.characterSelected.name == "Player2")
            {
                male.transform.localPosition = new Vector3(1.85f, -0.419f, 0f);
            }
            StartCoroutine(ObjectiveEnceladus());
            Physics.gravity = new Vector3(0, -20.0F, 0);
            stuff[7].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            stuff[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            stuff[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
		gameCamera.Init (player);
		print("controller.planetSelected.name_: " + controller.planetSelected.name);
    }



    // Update is called once per frame
  
}
