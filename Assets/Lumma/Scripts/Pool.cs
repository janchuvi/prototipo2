using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    private List<GameObject> meteorites = new List<GameObject>();
    public GameObject prefab;
    public GameObject Get()
    {
        GameObject go = null;
        if (meteorites.Count > 0)
        {
            go = meteorites[0];
            meteorites.Remove(go);
        }
        else
        {
            go = Instantiate(prefab);
        }
        go.SetActive(true);
        go.SendMessage("OutPool", this, SendMessageOptions.DontRequireReceiver);
        return go;
    }
	
	// Update is called once per frame
	public void Return(GameObject go)
    {
        go.SetActive(false);
        go.SendMessage("InPool", this, SendMessageOptions.DontRequireReceiver);
        meteorites.Add(go);
    }
}
