﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

	Player player;
	public Camera myCamera;
	public float offset = 2.5f;
	public GameObject sceneObjectActive;

	void Start()
	{
		Events.SceneObjectActive += SceneObjectActive;
		Events.OnTeleport += OnTeleport;
	}
	void OnDestroy()
	{
		Events.SceneObjectActive -= SceneObjectActive;
		Events.OnTeleport -=  OnTeleport;
	}
	void SceneObjectActive(GameObject go)
	{
		sceneObjectActive = go;
	}
	public void Init(Player _player)
	{
		this.player = _player;
		myCamera.transform.localPosition = new Vector3(0,2.2f,0);
	}
	bool isTeleporting;
	void OnTeleport()
	{
		isTeleporting = true;
		Invoke ("ResetTeleport", 0.5f);
	}
	void ResetTeleport()
	{
		isTeleporting = false;
	}
	void Update()
	{
		if (player == null)
			return;
		if (isTeleporting) {
			transform.position = player.transform.position;
		} else
		if (sceneObjectActive == null) {
			myCamera.transform.LookAt (player.head.transform);
		
			Vector3 pos = player.transform.position + offset * (player.transform.forward * -1);
			transform.position = Vector3.Lerp (transform.position, pos, 0.05f);
		} else {
			myCamera.transform.LookAt (sceneObjectActive.transform);
			Vector3 pos = sceneObjectActive.transform.position + 12 * (player.transform.up);
			transform.position = Vector3.Lerp (transform.position, pos, 0.005f);
		}
	}
}
