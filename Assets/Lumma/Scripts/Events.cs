using UnityEngine;
using System.Collections;

public static class Events {

    //The game:
	public static System.Action<string> OnLog = delegate { };
	public static System.Action<int> AddPoints = delegate { };
	public static System.Action<GameObject> SceneObjectActive = delegate { };

}
