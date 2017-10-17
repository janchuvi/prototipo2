using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterControllerScript : MonoBehaviour {
	public planets planet;
	public enum planets{
		MOON,
		ENCELADO
	}
	public GameObject moon;
	public GameObject encelado;

	public Player player1;
	public Player player2;
	public Player player3;

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
       
		player1.gameObject.SetActive (false);
		player2.gameObject.SetActive (false);
		player3.gameObject.SetActive (false);

        if (controller.characterSelected.name == "Player1")
        {
			player = player1;
			player.gameObject.SetActive (true);
            legs = new GameObject[4];
            legs[0] = GameObject.Find("Base HumanRThigh");
            legs[1] = GameObject.Find("Base HumanLThigh");
            legs[2] = GameObject.Find("Base HumanRCalf");
            legs[3] = GameObject.Find("Base HumanLCalf");
            male = GameObject.Find("PreBase").transform.GetChild(0);
            //   male = GameObject.Find("Male");
        }
        else if (controller.characterSelected.name == "Player2")
        {
			player = player2;
			player.gameObject.SetActive (true);
            legs = new GameObject[4];
            male = GameObject.Find("PreBase").transform.GetChild(0);
          //  male = GameObject.Find("Male");
            legs[0] = GameObject.Find("Base AlienRThigh");
            legs[1] = GameObject.Find("Base AlienLThigh");
            legs[2] = GameObject.Find("Base AlienRCalf");
            legs[3] = GameObject.Find("Base AlienLCalf");
            //  anim = stuff[1].GetComponent<Animation>();
        }
        else if (controller.characterSelected.name == "Player3")
        {
			player = player3;
			player.gameObject.SetActive (true);
            legs = new GameObject[4];
            legs[0] = GameObject.Find("Base HumanRThigh");
            legs[1] = GameObject.Find("Base HumanLThigh");
            legs[2] = GameObject.Find("Base HumanRCalf");
            legs[3] = GameObject.Find("Base HumanLCalf");
            male = GameObject.Find("PreBase").transform.GetChild(0);
            //    male = GameObject.Find("Male");
        }
        if (controller.planetSelected.name == "Terrain1")
        {//mars
        //    stuff[2].SetActive(true);
           
        }
        else if(controller.planetSelected.name == "Terrain2")
        {//moon
			planet = planets.MOON;
            RenderSettings.skybox = sky[0];
			moon.SetActive (true);

            StartCoroutine(ObjectiveMoon());
            Physics.gravity = new Vector3(0, -100.0F, 0);

        }
        else if (controller.planetSelected.name == "Terrain3")
        {//enceladus
			planet = planets.ENCELADO;
			encelado.SetActive (true);
            RenderSettings.skybox = sky[1];
            //  waist.transform.eulerAngles = new Vector3(0,90,0);
            player.turbine[0] = GameObject.Find("Turbine");
            player.turbine[1] = GameObject.Find("Turbine2");
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

         	player1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			player2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			player3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
		player.Init (this);
		gameCamera.Init (player);
    }
  
}
