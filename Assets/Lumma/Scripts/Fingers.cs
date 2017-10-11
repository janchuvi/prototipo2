using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightBuzz.Vitruvius.Avateering;
using Windows.Kinect;
//using LightBuzz.Vitruvius.FingerTracking;
using LightBuzz.Vitruvius;

public class Fingers : MonoBehaviour {
    public AvateeringSample sampleAvatar;
    public Body player;
	// Use this for initialization
	void Start () {
        sampleAvatar = GameObject.Find("Avateering Sample").GetComponent<AvateeringSample>();
        player = sampleAvatar.user;
        Windows.Kinect.Joint handLeft = player.Joints[JointType.HandLeft];
        Windows.Kinect.Joint handRight = player.Joints[JointType.HandRight];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
