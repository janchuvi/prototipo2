using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour {
	
	public Text field;
	// Use this for initialization
	void Start () {
		Events.OnLog += OnLog;
	}
	void OnDestroy () {
		Events.OnLog -= OnLog;
	}

	void OnLog (string loggedText) {
		field.text = loggedText;
	}
}
