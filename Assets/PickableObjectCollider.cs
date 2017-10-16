using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectCollider : MonoBehaviour {

	public PickableObejct pickableObject;

	void OnTriggerEnter(Collider other) {
		Player player = other.GetComponent<Player>();
		if (player != null) {
			//GetComponent<Collider> ().enabled = false;
			pickableObject.SetOn (player);
		}
	}
}
