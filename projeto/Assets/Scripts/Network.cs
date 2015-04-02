using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Network : MonoBehaviour {

	public    const     float             vel_proc    =  25.0f;  // 25kbs
	public    const     float             pck_size    =  100.0f; // each packge has 100kbits of data
	public    const     int               buff_size   =  4;	
	public  			Transform         node_prefab;
	public  			List<Transform>   graph;
	private  			List<Transform>   explored_set;
	private             List<Transform>   frontier_set;
	private				Transform         final_state;
	// Use this for initialization
	void Start () 
	{
		Transform obj1, obj2, obj3, obj4, obj5, obj6;
		obj1 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj2 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj3 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj4 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj5 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj6 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;

		obj1.GetComponent<Node>().set_up("A", Node.node_state.INITIAL, new Vector3(-8.2f, 0.0f, 0.0f),
		                                 1.0f, 2);


		obj2.GetComponent<Node>().set_up("F", Node.node_state.FINAL, new Vector3 (8.2f, 0.0f, 0.0f),
		                                 1.0f, 3);

		obj3.GetComponent<Node>().set_up("E", Node.node_state.INTERMEDIARY, new Vector3 (5.2f, -4.5f, 0.0f),
		                                 1.0f, 1);


		obj4.GetComponent<Node>().set_up("D", Node.node_state.INTERMEDIARY, new Vector3 (5.2f, 4.5f, 0.0f),
		                                 1.0f, 4);


		obj5.GetComponent<Node>().set_up("B", Node.node_state.INTERMEDIARY, new Vector3 (-4.5f, 4.5f, 0.0f),
		                                 1.0f, 2);


		obj6.GetComponent<Node>().set_up("C", Node.node_state.INTERMEDIARY, new Vector3 (-4.5f, -4.5f, 0.0f),
		                                 1.0f, 1);



		final_state = obj2;

		obj1.GetComponent<Node> ().insert_sucessor (obj5);
		obj1.GetComponent<Node> ().insert_sucessor (obj6);

		obj3.GetComponent<Node> ().insert_sucessor (obj2);

		obj4.GetComponent<Node> ().insert_sucessor (obj2);

		obj5.GetComponent<Node> ().insert_sucessor (obj4);

		obj6.GetComponent<Node> ().insert_sucessor (obj3);

		graph.Add (obj1);
		graph.Add (obj2);
		graph.Add (obj3);
		graph.Add (obj4);
		graph.Add (obj5);
		graph.Add (obj6);

		//Calculate the maximum distance to the final node for all the nodes in the graph

		for(int i = 0; i!= graph.Count; ++i){
			graph[i].GetComponent<Node>().set_dist( max_dist(graph[i], final_state) );
			Debug.Log("MD( " + graph[i].GetComponent<Node>().id + ") = " +  graph[i].GetComponent<Node>().get_dist());
		}



	}
	
	int max_dist(Transform fst_node , Transform last_node)
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


	void Update () {
	
	}

	
}
