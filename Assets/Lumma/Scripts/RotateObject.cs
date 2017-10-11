using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public Vector3 direction = new Vector3(0.0f, 0.0f, 1.0f);
    public float rpm = 0.1f;

	// Update is called once per frame
	void Update () {
        transform.Rotate(direction * rpm * Time.deltaTime);
    }
}
