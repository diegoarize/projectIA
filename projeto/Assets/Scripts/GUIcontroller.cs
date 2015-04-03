using UnityEngine;
using System.Collections;

public class GUIcontroller : MonoBehaviour {
	private Network net;

	void start() {
		net = gameObject.GetComponent<Network> ();
	}

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,110,95), "Network Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,90,20), "Create node")) {
			Debug.Log("create a node");
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(20,70,90,20), "Create Edge")) {
			Debug.Log("create an edge");
		}

		// Make the third button.
		if(GUI.Button(new Rect(20,110,90,20), "Send packet")) {
			Debug.Log("sending a packet");
		}
	}
}
