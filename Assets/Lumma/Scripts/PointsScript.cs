using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsScript : MonoBehaviour {
	
    int points;
    public Text pointsText;

    void Start() {
		Events.AddPoints += AddPoints;
	}
	void OnEnable() {
		points = 0;
		SetPoints ();
	}
	void OnDestroy() {
		Events.AddPoints -= AddPoints;
	}

	void AddPoints (int _points) {
		points += _points;
		SetPoints ();
	}
	void SetPoints()
	{
		pointsText.text = "Points: "+ points.ToString();
	}
}
