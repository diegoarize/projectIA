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

	public int max_dist(Transform fst_node , Transform last_node)
	{
		int max = 0;
		int dist     = 0;
		
		if (fst_node == last_node) {
			return 0;
		}
		
		for (int i = 0; i != fst_node.GetComponent<Node>().successors.Count; ++i) {
			//Debug.Log("In node " + fst_node.GetComponent<Node>().id);
			++dist;
			dist = dist + max_dist(fst_node.GetComponent<Node>().successors[i], last_node);
			max = (dist > max) ? (dist) :(max);
			dist = 0;
		}
		return max;
	}
}
