using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour {

    public DataController dataController;
    public Text text;
    public int time;
    public GameObject[] img;
    public GameObject[] img2;
    void Start () {
        //img = new GameObject[3];
      //  img2 = new GameObject[3];
        dataController = GameObject.Find("DataController").GetComponent<DataController>();
        if (dataController.planetSelected.name == "Terrain2")
        {//moon
            text.text = "Para controlar el vehiculo lunar utiliza los siguientes movimientos: \n Tienes 2 minutos para explorar la Luna";
            img[0].SetActive(true);
            img[1].SetActive(true);
            img[2].SetActive(true);
            StartCoroutine(Game());
        }
        else if (dataController.planetSelected.name == "Terrain3")
        {//enceladus
            text.text = "Para controlar la patineta gravitatoria utiliza los siguientes movimientos: \n Tienes 2 minutos para explorar Enceladus";
            img2[0].SetActive(true);
            img2[1].SetActive(true);
            img2[2].SetActive(true);
            StartCoroutine(Game());
        }
    }
	
	// Update is called once per frame
IEnumerator Game()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Game");
    }
}
