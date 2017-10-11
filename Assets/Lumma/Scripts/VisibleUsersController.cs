using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleUsersController : MonoBehaviour {

    public CursorSample cursorSample;
    public SimpleObjectPool usersPool;
    private List<GameObject> usersObjects = new List<GameObject>();
    public StartScreenController screenController;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        // Return old users
        while (usersObjects.Count > 0)
        {
            usersPool.ReturnObject(usersObjects[0]);
            usersObjects.RemoveAt(0);
        }

        Windows.Kinect.Body[] bodies = cursorSample.GetAllBodies();
        int playerBody = cursorSample.GetPlayerBodyNum();
        for (int i=0; i<bodies.Length; i++)
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
                    renderer.color = Color.red;
                    screenController.StartGame();
                }
                else
                {
                    renderer.color = Color.white;
                }
            }
        }
    }
}
