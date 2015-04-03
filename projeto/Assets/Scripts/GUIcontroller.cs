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
			if (createNodeClicked) {
				mousePosition = Input.mousePosition;
				createNode ();
				createNodeClicked = false;
			} else if(createEdgeClicked) {
				if(parentNode == null) {
					parentNode = getClickedNode();
					Debug.Log("parent node");
					//TODO: Fazer o highlight do node clicado
				} else {
					Debug.Log("child node");
					childNode = getClickedNode();
					//TODO: Fazer o highlight do node clicado e depois voltar os dois pra o normal
					createEdge();
					createEdgeClicked = false;
					//clear the Nodes after create the edge
					parentNode = childNode = null;
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

	private Transform getClickedNode() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit) {
			return hit.collider.gameObject.transform;
		}
		return null;
	}
}
