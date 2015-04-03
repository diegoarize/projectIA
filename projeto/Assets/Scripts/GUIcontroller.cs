using UnityEngine;
using System.Collections;

public class GUIcontroller : MonoBehaviour {
	private Network net;
	private Vector3 mousePosition;
	private bool createNodeClicked = false;
	private bool createEdgeClicked = false;
	private Transform parentNode, childNode;

	void Start() {
		net = gameObject.GetComponent<Network> ();
		parentNode = childNode = null;
	}

	void Update() {

		if (Input.GetMouseButtonDown(0)) {
			//getClickedNode ();
			if (createNodeClicked) {
					createNode ();
					createNodeClicked = false;
			} else if(createEdgeClicked) {
				if(parentNode == null) {
					//TODO: captura objeto
				} else {
					//TODO: captura objeto pra o child
					createEdge();
					createEdgeClicked = false;
				}
			}
		}
	}

	void createNode() {
		Debug.Log("create a node " + mousePosition);
	}

	void createEdge() {
		Debug.Log("create an edge");
	}

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,110,95), "Network Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,90,20), "Create node")) {
			createNodeClicked = true;
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(20,70,90,20), "Create Edge")) {
			createEdgeClicked = true;
		}

		// Make the third button.
		if(GUI.Button(new Rect(20,110,95,20), "Send pkt DFS")) {
			Debug.Log("sending a packet DFS");
		}
		if(GUI.Button(new Rect(20,140,95,20), "Send pkt A*")) {
			Debug.Log("sending a packet A*");
		}
	}

	void getClickedNode() {
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		
		if (hit.collider != null) {
			Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
		}
	}
}
