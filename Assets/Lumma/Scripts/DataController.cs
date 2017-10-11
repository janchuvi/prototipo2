using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

    public GameObject characterSelected;
    public GameObject planetSelected;
    private static DataController playerInstance;
    bool charSelec;
    bool planSelect;
    bool nada;
    public int time;
    // Use this for initialization
     void Start ()
    {
        DontDestroyOnLoad(gameObject);
        nada = false;
        charSelec = false;
        planSelect = false;
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            DestroyObject(GameObject.Find("DataController"));
        } 
    }
    IEnumerator Game()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MoonSelection");
        nada = true;
        Debug.Log("papas");
    }

     void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (charSelec == true && currentScene.name == "CharacterSelection")
        {
            SceneManager.LoadScene("Showcase");
        }
        else if(planSelect == true && currentScene.name == "MoonSelection")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (currentScene.name == "Showcase" )
        {
                StartCoroutine(Game());
        }
        else if(currentScene.name == "Game" || currentScene.name == "Tutorial" || currentScene.name == "MoonSelection")
        {
            StopAllCoroutines();
        }
        else if (currentScene.name == "StartScreen")
        {
            Reboot();
        }
    }
    public void SetSelectedPlanet(GameObject selectedGameObject)
    {
        planetSelected = selectedGameObject;
        planSelect = true;
    }
    public void SetSelectedPlayer(GameObject selectedGameObject)
    {
        characterSelected = selectedGameObject;
        charSelec = true;
    }
    private void Reboot()
    {
        characterSelected = null;
        planetSelected = null;
        planSelect = false;
        charSelec = false;
    }
}
