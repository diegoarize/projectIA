using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class algorithms : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	    
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public List<Transform> DFS (Transform firstNode, Transform lastNode)
		{
				List<Transform> l = null;
				if (firstNode == lastNode) {
						l = new List<Transform> ();
						l.Add (firstNode);
						return l;
				} else {
						foreach (Transform n in firstNode.GetComponent<Node>().successors) {
								l = l.Concat (DFS (n, lastNode)).ToList ();
						}
				}
				return l;
		}

		public int max_dist (Node fst_node, Node last_node)
		{
				int max = 0;
				int dist = 0;
		
				if (fst_node == last_node) {
						return 0;
				}
		
				for (int i = 0; i != fst_node.successors.Count; ++i) {
						//Debug.Log("In node " + fst_node.GetComponent<Node>().id);
						++dist;
						dist = dist + max_dist (fst_node.successors [i].GetComponent<Node>(), last_node);
						max = (dist > max) ? (dist) : (max);
						dist = 0;
				}
				return max;
		}

	public List<Transform> aStar(Transform firstNodeTransform, Transform lastNodeTransform)
	{
		List<Node> expandedNodes = new List<Node> ();
		Node firstNode = firstNodeTransform.GetComponent<Node>();
		Node lastNode = lastNodeTransform.GetComponent<Node>();
		Node smallerDistance = firstNode;
		//expandedNodes.Add(smallerDistance);

		foreach (Transform node in firstNode.successors)
		{
			//TODO: usar o GetComponent<Node>()
		}

		return null;
	}

	private float calculateHeuristic(Node start, Node endNode)
	{
		//TODO: verificar se eh realmente isso ai
		return (max_dist (start, endNode)) * ((Network.buff_size / 2) * Network.pck_size / Network.vel_proc);
	}

	private float calculateCost(Node first, Node current)
	{
		//TODO: implementar o custo ate o no atual
		return 0;
	}
}
