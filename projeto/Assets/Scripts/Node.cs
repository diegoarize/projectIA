using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	//Reserved for heuristics calculations
	private    int  	          dist_to_final; //number of nodes till the end

	public float g; // cost of this node + it's predecessors
	public float h; // heuristic estimate of distance to goal
	public float f; // sum of cumulative cost of predecessors and self and heuristic

	//Reserved for heuristics calculations

	private    int                qtd_pck;
	public     string             id;
	private    Vector3            prev_pos;    
	public     List<Transform>    successors;
	public     List<Transform>    predecessor;
	private    float              link_vel;// velocity in kbits/seg
	public     enum node_state    {INITIAL, FINAL, INTERMEDIARY};
	public     node_state         state  =  node_state.INITIAL;  
	public     Transform           edge_prefab;
	public     List<Transform>     links;   
	private    bool                successor_moved;


	public void set_up(string id, node_state s, Vector3 pos, float link_vel,
	              int qtd_pck)
	{
		this.id             =  id;
		this.state          =  s;
		transform.position  =  pos;
		prev_pos            =  pos;
		this.link_vel       =  link_vel;
		this.qtd_pck        =  qtd_pck;
		successor_moved      =  false;
	}


	void Update ()
	{
		if (prev_pos != transform.position || successor_moved ) {
			for (int i = 0; i != links.Count; ++i) {
				links[i].GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
				links[i].GetComponent<LineRenderer>().SetPosition(1, successors[i].transform.position);
			}
			prev_pos         = transform.position;
			successor_moved  =  false;
			for (int i = 0; i!= predecessor.Count; ++i) {
				predecessor[i].GetComponent<Node>().set_successor_moved(true);
			}
		}
	}

	public void set_state(node_state s) 
	{
		state = s;
	}

	public void  set_successor_moved(bool l_value)
	{
		successor_moved = true;
	}

	public int get_dist()
	{
		return dist_to_final;
	}

	public void set_dist(int dist)
	{
		dist_to_final = dist;
	}

	public void set_position(Vector3 pos) 
	{
		this.transform.position = pos;
	}

	public void set_link_vel(float vel) 
	{
		link_vel  =  vel;
	}


	public void insert_sucessor(Transform n)
	{
		Transform obj;

		obj = Instantiate (edge_prefab, this.transform.position, Quaternion.identity) as Transform;
		obj.GetComponent<LineRenderer> ().SetVertexCount (2);
		obj.GetComponent<LineRenderer> ().SetPosition (0, this.transform.position);
		obj.GetComponent<LineRenderer> ().SetPosition (1, n.transform.position);
		obj.GetComponent<LineRenderer>().material = new Material (Shader.Find("Particles/Additive"));
		obj.GetComponent<LineRenderer> ().SetColors (Color.red, Color.red);
		obj.GetComponent<LineRenderer> ().SetWidth (0.1f, 0.1f);
		links.Add (obj);
		successors.Add (n);

		n.GetComponent<Node> ().predecessor.Add (this.transform);
	}


	public bool  is_final()
	{
		return (state == node_state.FINAL) ? (true) :(false);
	}


	public void set_qtd_package(int qtd) 
	{
		qtd_pck = qtd;
	}

	/* time_to_reach: time for a packet of size 'pck_size'
	 * to get into the node using the node's only link
	 */
	public float time_to_reach(float pck_size) 
	{
		return pck_size / link_vel;
	} 

	/* time_processing: time to process 'qtd_pck' packets
	 * of size 'pck_size' with a processing velocity of
	 * 'vel'
	 */
	public float time_processing(float vel, float pck_size) 
	{
		return qtd_pck * pck_size / vel;
	}

	public List<Transform> get_succerssors()
	{
		return successors;
	}

	public void set_id(string id) 
	{
		this.id = id;
	}
}
