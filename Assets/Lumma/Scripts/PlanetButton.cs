using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetButton : ClickeableButton
{

    public GameObject Planet;
    public DataController controller;
    public GameObject particle;
    private void Start()
    {
       
        particle.SetActive(false);
        controller = GameObject.Find("DataController").GetComponent<DataController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3) && this.gameObject.name == "Planet1")
        {
            controller.SetSelectedPlanet(Planet);
            particle.SetActive(true);

			Debug.Log("Planet1");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) && this.gameObject.name == "Planet3")
        {
            controller.SetSelectedPlanet(Planet);
            particle.SetActive(true);
			Debug.Log("Planet3");
        }
    }
    protected override void OnClick()
    {
        controller.SetSelectedPlanet(Planet);
        particle.SetActive(true);

		Debug.Log("OnClick");
      //  SceneManager.LoadScene("Game");
    }

}
