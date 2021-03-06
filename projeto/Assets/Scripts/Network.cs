﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Network : MonoBehaviour {

	public    const     float             vel_proc    =  25.0f;  // proccessing speed of every node is 25kbs
	public    const     float             pck_size    =  100.0f; // each packge has 100kbits of data
	public    const     int               buff_size   =  4;	     // each buffer has space for four packets
	public    const     float 			  link_speed  =  1.0f;
	public  			Transform         node_prefab;
	public  			List<Transform>   graph;
	private  			List<Transform>   explored_set;
	private             List<Transform>   frontier_set;
	private				Transform         final_state;
	private   			Transform         curr_state;
	private             Color             hightlight_color;
	public        	    Color             default_color;
	private 			int					nodeCount = 0;//nodes' id counter

	public  Camera cam;

	public				int              MIN_DIST = -1;

	// Use this for initialization
	void Start () 
	{
		/*
		
		Transform obj1, obj2, obj3, obj4, obj5, obj6;
		obj1 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj2 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj3 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj4 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj5 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		obj6 = Instantiate (node_prefab, transform.position, Quaternion.identity) as Transform;
		
		obj1.GetComponent<Node>().set_up("A", Node.node_state.INITIAL, new Vector3(-8.2f, 0.0f, 0.0f),
		                                 link_speed, 2,vel_proc);
		
		
		obj2.GetComponent<Node>().set_up("F", Node.node_state.FINAL, new Vector3 (8.2f, 0.0f, 0.0f),
		                                 link_speed, 3, vel_proc);
		
		obj3.GetComponent<Node>().set_up("E", Node.node_state.INTERMEDIARY, new Vector3 (5.2f, -4.5f, 0.0f),
		                                 link_speed, 1, vel_proc);
		
		
		obj4.GetComponent<Node>().set_up("D", Node.node_state.INTERMEDIARY, new Vector3 (5.2f, 4.5f, 0.0f),
		                                 link_speed, 4, vel_proc);
		
		
		obj5.GetComponent<Node>().set_up("B", Node.node_state.INTERMEDIARY, new Vector3 (-4.5f, 4.5f, 0.0f),
		                                 link_speed, 2, vel_proc);
		
		
		obj6.GetComponent<Node>().set_up("C", Node.node_state.INTERMEDIARY, new Vector3 (-4.5f, -4.5f, 0.0f),
		                                 link_speed, 1, vel_proc);
		
		
		curr_state  = obj1;
		final_state = obj2;
		
		obj1.GetComponent<Node> ().insert_sucessor (obj5);
		obj1.GetComponent<Node> ().insert_sucessor (obj6);
		
		
		obj3.GetComponent<Node> ().insert_sucessor (obj4);
		obj3.GetComponent<Node> ().insert_sucessor (obj2);
		
		obj4.GetComponent<Node> ().insert_sucessor (obj2);
		obj4.GetComponent<Node> ().insert_sucessor (obj3);
		
		obj5.GetComponent<Node> ().insert_sucessor (obj4);
		
		obj6.GetComponent<Node> ().insert_sucessor (obj3);
		
		graph.Add (obj1);
		graph.Add (obj2);
		graph.Add (obj3);
		graph.Add (obj4);
		graph.Add (obj5);
		graph.Add (obj6);
		
		this.frontier_set = new List<Transform> ();
		this.explored_set = new List<Transform> ();
		default_color     =  Color.black;
		hightlight_color  =  Color.white;
		//Calculate the maximum distance to the final node for all the nodes in the graph
		
		
		List<int> a = new List<int> ();;
		for (int i = 0; i != graph.Count; ++i) {
			distance (graph,graph[i],final_state);	
			a.Add(graph[i].GetComponent<Node>().get_dist());
		}
		for (int i = 0; i!= a.Count; ++i) {
			graph[i].GetComponent<Node>().set_dist(a[i]);
		}

*/
	}
	/*
	 * Calculate the maximum distance to the final node for all the nodes in the graph
	 * 
	 */

	private void calculateDistances()
	{
		this.frontier_set = new List<Transform> ();
		this.explored_set = new List<Transform> ();
		default_color     =  Color.black;
		hightlight_color  =  Color.white;
		//Calculate the maximum distance to the final node for all the nodes in the graph
		
		
		List<int> a = new List<int> ();;
		for (int i = 0; i != graph.Count; ++i) {
			distance (graph,graph[i],final_state);	
			a.Add(graph[i].GetComponent<Node>().get_dist());
		}
		for (int i = 0; i!= a.Count; ++i) {
			graph[i].GetComponent<Node>().set_dist(a[i]);
		}
	}

	/**
	 * It creates a node on the screen
	 * @param Vector3 - mousePosition
	 * @return Transform - created Node
	 */
	public Transform createNode(Vector3 mousePosition)
	{
		Debug.Log (mousePosition);
		Transform obj = Instantiate (node_prefab, Input.mousePosition, Quaternion.identity) as Transform;
		Debug.Log(obj);
		mousePosition [2] = 5.0f;

		if (nodeCount == 0) {
			//first Node
			obj.GetComponent<Node> ().set_up (""+nodeCount, Node.node_state.INITIAL, cam.ScreenToWorldPoint(mousePosition) ,
			                                link_speed, 2, vel_proc);
			curr_state = obj.transform;//estado inicial
			nodeCount++;
		} else if (nodeCount == 1) {
			obj.GetComponent<Node> ().set_up (""+nodeCount, Node.node_state.FINAL,cam.ScreenToWorldPoint(mousePosition),
			                                link_speed, 2, vel_proc);
			final_state = obj.transform;//estado final
			nodeCount++;
		} else {
			graph.ElementAt (nodeCount - 1).GetComponent<Node> ().set_state (Node.node_state.INTERMEDIARY);
			obj.GetComponent<Node> ().set_up (""+nodeCount, Node.node_state.FINAL, cam.ScreenToWorldPoint(mousePosition),
			                                  link_speed, 2, vel_proc);
			final_state = obj.transform;//estado final
			nodeCount++;
		}
		return obj;
	}
	public void setQtdPkts(Transform node, int qtd_pkt)
	{
		node.GetComponent<Node> ().set_qtd_package (qtd_pkt);
	}
	/*
	 * inserts a node into the network
	 * @param Node - node
	 * 
	 */
	public void insertOnNetwork(Transform node)
	{
		graph.Add (node);
	}

	/*
	 * inserts the child node to the parent node
	 * successors list
	 * @param Transform - parentNode
	 * @param Transform - childNode
	 * 
	 */
	public void insertSuccessor(Transform parentNode, Transform childNode)
	{
		parentNode.GetComponent<Node> ().insert_sucessor (childNode);
	}
	/*
	 * executes the DFS algorithm
	 * 
	 */
	public void DFS()
	{
		calculateDistances ();
		D_first_search (curr_state);//Pegar o retorno caso necessario
	}

	/*
	 * executes the A* algorithm
	 * 
	 */
	public void aStar()
	{
		calculateDistances ();
		a_star (curr_state);//Pegar o retorno caso necessario
	}

	Transform get_larger_dist(List<Transform> g ) 
	{
		Transform node = null;
		
		for ( int i = 0; i != g.Count ; ++i) {
			if ( node == null) {
				node = g[i];
			}
			else {
				if( g[i].GetComponent<Node>().get_dist() > node.GetComponent<Node>().get_dist() ) {
					node = g[i];
				}
			}
		}
		return node;
	}
	
	
	void distance(List<Transform> g, Transform initial_state, Transform final_state) 
	{
		List<Transform> unvisited_set = new List<Transform> ();
		Node_Comparer node_Comparer = new Node_Comparer();
		
		for (int i = 0; i != g.Count; ++i) {
			g[i].GetComponent<Node>().set_dist(MIN_DIST);
			unvisited_set.Add(g[i]);
		}
		initial_state.GetComponent<Node> ().set_dist (0);
		Transform current_node = initial_state;
		int j = 0;
		while (true) {
			Debug.Log(current_node.GetComponent<Node>().id);
			for (int i = 0; i != current_node.GetComponent<Node>().successors.Count; ++i) {
				Transform child = current_node.GetComponent<Node>().successors[i];
				if( child.GetComponent<Node>().get_dist() < current_node.GetComponent<Node>().get_dist() + 1){
					child.GetComponent<Node>().set_dist(current_node.GetComponent<Node>().get_dist() + 1);
				}
			}
			int index;
			index = unvisited_set.FindIndex (x => x.GetComponent<Node> ().id == current_node.GetComponent<Node> ().id ); 
			unvisited_set.RemoveAt (index);
			
			
			current_node = get_larger_dist(unvisited_set);
			if(  current_node == null || current_node.GetComponent<Node>().get_dist() == MIN_DIST ){
				initial_state.GetComponent<Node>().set_dist( final_state.GetComponent<Node>().get_dist() );
				final_state.GetComponent<Node>().set_dist(0);
				return;
			}
		}
	}

	public void set_heuristic(Transform node)
	{
		node.GetComponent<Node>().h = node.GetComponent<Node> ().get_dist() * ((pck_size / vel_proc) * (buff_size / 2));
		//return node.GetComponent<Node> ().get_dist * ((pck_size / vel_proc) * (buff_size / 2));
	}

	public void set_g(Transform node) 
	{
		node.GetComponent<Node> ().g = node.GetComponent<Node> ().time_processing (pck_size);
		if (node.GetComponent<Node> ().parent != null) {
			node.GetComponent<Node>().g += node.GetComponent<Node>().parent.GetComponent<Node>().g;
		}
	}

	public float a_star(Transform initial_state) 
	{
		Node_Comparer node_Comparer = new Node_Comparer(); 
		//List<string> solution = new List<string> ();
		Transform node = initial_state;

		if (frontier_set.Count != 0) {
			frontier_set.Clear ();
			
		}
		if (explored_set.Count != 0) {
			explored_set.Clear ();
			
		}
		frontier_set.Add(node);

		set_heuristic(node);
		set_g(node);

		Debug.Log ("Path is:");

		while(true) {
			if (frontier_set.Count == 0) {
				return -1;;
			}
			node  =  frontier_set[0];
			frontier_set.RemoveAt(0);
			Debug.Log(node.GetComponent<Node>().id);

			if (node.GetComponent<Node>().state == Node.node_state.FINAL) {
				return node.GetComponent<Node>().get_f();
			}
			explored_set.Add(node);

			for (int i = 0; i!= node.GetComponent<Node>().successors.Count; ++i) {
				Transform child  =  node.GetComponent<Node>().successors[i];
				child.GetComponent<Node>().parent  =  node;
				if ( !(explored_set.Find( x => x.GetComponent<Node>().id == child.GetComponent<Node>().id) ||
				       frontier_set.Find( x => x.GetComponent<Node>().id == child.GetComponent<Node>().id) ) ) {
					set_heuristic(child);
					set_g(child);
					frontier_set.Add(child);
				}
			}
			frontier_set.Sort(node_Comparer);
		}
	} 

/*	//Remover daqui quando implementado em Algorithms
	//ao inves de chamar esse chamar o de Algorithms
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
	}*/


	void Update () {
	
	}

	Transform D_first_search(Transform initial_state)
	{
		Transform node = initial_state;
		
		if (frontier_set.Count != 0) {
			frontier_set.Clear ();
			
		}
		if (explored_set.Count != 0) {
			explored_set.Clear ();
			
		}
		frontier_set.Add(node);
		set_g (node);

		Debug.Log ("Path is:");

		while(true) {
			if (frontier_set.Count == 0) {
				return null;
			}
			node  =  frontier_set[0];
			frontier_set.RemoveAt(0);
			Debug.Log(node.GetComponent<Node>().id);

			if(node.GetComponent<Node>().state == Node.node_state.FINAL){
				if(node.GetComponent<Node>().parent_edge != null){
					node.GetComponent<Node>().parent_edge.GetComponent<LineRenderer>().SetColors(hightlight_color,hightlight_color);
					node.GetComponent<Node>().parent_arrow_head.GetComponent<LineRenderer>().SetColors(hightlight_color, hightlight_color);
				}
				return node;
			}
			else{
				explored_set.Add(node);
				if(node.GetComponent<Node>().parent_edge != null){
					node.GetComponent<Node>().parent_edge.GetComponent<LineRenderer>().SetColors(hightlight_color,hightlight_color);
					node.GetComponent<Node>().parent_arrow_head.GetComponent<LineRenderer>().SetColors(hightlight_color, hightlight_color);
				}
			}
			
			
			for(int i = 0; i != node.GetComponent<Node>().successors.Count; ++i){
				Transform child = node.GetComponent<Node>().successors[i];
	
				if ( !(explored_set.Find( x => x.GetComponent<Node>().id == child.GetComponent<Node>().id) ||
				       frontier_set.Find( x => x.GetComponent<Node>().id == child.GetComponent<Node>().id) ) ) {
					child.GetComponent<Node>().parent_edge        =  node.GetComponent<Node>().links[i];
					child.GetComponent<Node>().parent_arrow_head  =  node.GetComponent<Node>().arrow_heads[i];
					frontier_set.Insert(0, child);
					set_g(child);
					child.GetComponent<Node>().parent = node;
				}
				
			}
			
		}
	}


	
}
