using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterButtons : ClickeableButton

{

    public GameObject player;
   public  DataController controller;
    public GameObject particle;
    void Start () {
        particle.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)&& this.gameObject.name == "Character1")
        {
            controller.SetSelectedPlayer(player);
           // particle.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) && this.gameObject.name == "Character2")
        {
            controller.SetSelectedPlayer(player);
          //  particle.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) && this.gameObject.name == "Character3")
        {
            controller.SetSelectedPlayer(player);
         //   particle.SetActive(true);

        }
    }
    protected override void OnClick()
    {
        controller.SetSelectedPlayer(player);
      //  particle.SetActive(true);
        Debug.Log("lalalaalla");
    }

}
