using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
   public  FadeManager fadeManager;
    public void StartGame()
    {
        StartCoroutine(TimePass());
    }
    IEnumerator TimePass()
    {


        fadeManager.BeginFade(1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("CharacterSelection");
    }
}
