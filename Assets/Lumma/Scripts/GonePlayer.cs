using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GonePlayer : MonoBehaviour {

    public CursorSample cursorSample;
    public SimpleObjectPool usersPool;
    private List<GameObject> usersObjects = new List<GameObject>();
    int players;
     bool cambiar;
   public bool fui;
    public AvateeringSample avaterring;
   // public StartScreenController screenController;

    // Use this for initialization
    void Start()
    {
     // avaterring.models
    }

    // Update is called once per frame
    void Update()
    {
       
        // Return old users
        while (usersObjects.Count > 0)
        {
            usersPool.ReturnObject(usersObjects[0]);
            usersObjects.RemoveAt(0);
        }

        Windows.Kinect.Body[] bodies = cursorSample.GetAllBodies();
        int playerBody = cursorSample.GetPlayerBodyNum();
        players = playerBody;
    //    Debug.Log(playerBody);
        for (int i = 0; i < bodies.Length; i++)
        {
            Windows.Kinect.Body body = bodies[i];
            if (body.IsTracked)
            {
                // Add new users
                GameObject userObject = usersPool.GetObject();
                usersObjects.Add(userObject);
                userObject.transform.SetParent(transform.parent);
                Windows.Kinect.CameraSpacePoint csp = body.Joints[Windows.Kinect.JointType.Head].Position;
                // pos.y = 5 => csp.Z = 0  -   pos.y = 1.77 => csp.Z = 1.8
                // pos.x = 0 => csp.X = 0  -   pos.x = 1.0  => csp.X = 0.603
                Vector3 pos = new Vector3(csp.X * 1.6584f, csp.Z * -1.7944f + 5.0f, 0.0f);
                // Debug.Log("Usr: (" + csp.X.ToString() + "," + csp.Y.ToString() + "," + csp.Z.ToString() + ") - Pos: " + pos.ToString());
                userObject.transform.position = pos;
                SpriteRenderer renderer = userObject.GetComponent<SpriteRenderer>();
                if (playerBody == i)
                {
                    renderer.color = new Color(0f, 0f, 0f, 0f);
                    cambiar = false;
                    fui = false;
                    avaterring.models[0].updateModel = true;
                }
                else if(playerBody == -1)
                {
                    renderer.color = new Color(0f, 0f, 0f, 0f);
                    ChangeScenes();
                }
            }
        }
    }
    IEnumerator GoBack()
    {
        avaterring.models[0].updateModel = false;
        fui = true;
      //  Debug.Log("byebye");
        yield return new WaitForSeconds(10);
        cambiar = true;
        
    }
    void ChangeScenes()
    {
        StartCoroutine(GoBack());
        if (cambiar == true)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
}