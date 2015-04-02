using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class algorithms : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<Transform> DFS(Transform firstNode, Transform lastNode)
    {
		List<Transform> l = null;
		if (firstNode == lastNode) {
			l = new List<Transform> ();
			l.Add (firstNode);
			return l;
		} else {
			foreach (Transform n in firstNode.GetComponent<Node>().successors) {
				l = l.Concat(DFS(n, lastNode)).ToList();
			}
		}
		return l;
    }
}
