using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timeScript : MonoBehaviour {

    public Text timeText;
   
    public int maxTime = 120;
    private float remainingSeconds;
    private float slowDownTime;
    bool crecer = true;
    void Start () {
        ResetGame();
       

    }

    // Update is called once per frame
    void Update () {
		
	}
    void FixedUpdate()
    {   // FixedUpdate se llama antes de calcular Physics (movimientos)
        remainingSeconds -= Time.deltaTime;
        if (remainingSeconds <= 0.0f)
            remainingSeconds = 0.0f;
        UpdateTime();
        if (remainingSeconds == 0.0f)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
    public void ResetGame()
    {
       
        remainingSeconds = maxTime;
        slowDownTime = 0.0f;
       
    }
    void UpdateTime()
    {
        int minutes;
        int seconds;

        minutes = Mathf.FloorToInt(remainingSeconds / 60.0f);
        seconds = Mathf.FloorToInt(remainingSeconds % 60.0f);
        if (seconds <= 10)
        {

          //  if (crecer == true)
               // GrowTime();
        }

        timeText.text ="Tiempo: " + string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
    void GrowTime()
    {
        crecer = false;
        timeText.color = Color.red;
        timeText.fontSize = 20;
        StartCoroutine(TextTime());
    }
    IEnumerator TextTime()
    {

        yield return new WaitForSeconds(1f);
        timeText.fontSize = 25;
        crecer = true;
    }
}
