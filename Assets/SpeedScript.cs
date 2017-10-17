using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedScript : MonoBehaviour {

	public Text field;
	int speed = 0;
	void Start()
	{
		UpdateSpeed (0);
	}
	public void UpdateSpeed(int speed) {
		field.text = "Velocidad: " + speed + " km/h";
	}
}
