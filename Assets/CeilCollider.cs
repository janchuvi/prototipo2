using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilCollider : MonoBehaviour {

	Player player;
	void Start()
	{
		player = GetComponentInParent<Player>();
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "TerrainMoon" || collision.gameObject.name == "TerrainEncelado") {
			Vector3 pos = player.transform.position;
			pos.y += 2;
			player.transform.localEulerAngles = Vector3.zero;
			player.transform.position = pos;
			print ("OnCollisionEnter TerrainMoon");
		}
	}
}
