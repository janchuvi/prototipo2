using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObejct : MonoBehaviour {

	public Player player;

	public void SetOn(Player player)
	{
		this.player = player; 
		GetComponent<Collider> ().enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		Invoke ("Reset", 0.8f);
	}
	void Update () 
	{
		if (player == null)
			return;

		Vector3 dest = player.transform.position;
		transform.position = Vector3.Lerp (transform.position, dest, 0.1f);
	}
	void Reset()
	{
		Events.AddPoints (3);

		Vector3 newPos =  transform.position + new Vector3(0,4,0) + (player.transform.forward * 100);
		print ("pos: " + transform.position + "   newPos: " + newPos);
		transform.position = newPos;
		Invoke ("Restart", 0.1f);
		//GameObject.Destroy (this.gameObject);
		player = null;
	}
	void Restart()
	{
		GetComponent<Rigidbody> ().isKinematic = false;
		GetComponent<Collider> ().enabled = true;
	}
}
