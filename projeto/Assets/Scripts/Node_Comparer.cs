using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node_Comparer : IComparer<Transform> {

	public int Compare(Transform node1, Transform node2) 
	{
		if (node1.GetComponent<Node> ().get_f() > node2.GetComponent<Node> ().get_f()) {
			return 1;
		}
		else if (node1.GetComponent<Node> ().get_f() < node2.GetComponent<Node> ().get_f()) {
			return -1;
		}
		else {
			return 0;
		}
	}
}
