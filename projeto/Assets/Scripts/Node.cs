using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	//Reserved for heuristics calculations
	private    int  	          dist_to_final; //number of nodes till the end

	public float g; // cost of this node + it's predecessors
	public float h; // heuristic estimate of time distance to goal
	public float f; // sum of cumulative cost of predecessors and self and heuristic

	//Reserved for heuristics calculations

	public    int                qtd_pck;
	public     string             id;
	private    Vector3            prev_pos;    
	public     List<Transform>    successors;
	public     List<Transform>    predecessor;
	private    float              link_vel;// velocity in kbits/seg
	private	   float              node_vel;	
	public     enum node_state    {INITIAL, FINAL, INTERMEDIARY};
	public     node_state         state  =  node_state.INITIAL;  
	public     Transform           edge_prefab;
	public     List<Transform>     links;   
	public	   List<Transform>     arrow_heads;	
	private    bool                successor_moved;

	public	   Transform           parent;  // The node in the searcj tree that generated this node
	public     Transform           parent_edge;
	public     Transform           parent_arrow_head;

	public     Color              default_color;
	public     Color			  hightlight_color;	

	public void set_up(string id, node_state s, Vector3 pos, float link_vel,
	              int qtd_pck, float node_vel)
	{
		this.id             =  id;
		this.state          =  s;
		transform.position  =  pos;
		prev_pos            =  pos;
		this.link_vel       =  link_vel;
		this.node_vel       =  node_vel;
		this.qtd_pck        =  qtd_pck;
		successor_moved     =  false;
	
		default_color     =   Color.red;
		hightlight_color  =   Color.white;

		gameObject.renderer.material.SetColor("_Color", Color.grey);
	}


	void Update ()
	{
		if (prev_pos != transform.position || successor_moved ) {
			for (int i = 0; i != links.Count; ++i) {
				links[i].GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
				links[i].GetComponent<LineRenderer>().SetPosition(1, successors[i].transform.position);
			
				Vector3 pos = successors[i].transform.position - this.transform.position;
				pos [0] *= 0.8f;
				pos [1] *= 0.8f;
				pos += this.transform.position;

				arrow_heads[i].GetComponent<LineRenderer> ().SetPosition (0,pos);
				arrow_heads[i].GetComponent<LineRenderer> ().SetPosition (1, successors[i].transform.position);

			}
			prev_pos         = transform.position;
			successor_moved  =  false;
			for (int i = 0; i!= predecessor.Count; ++i) {
				predecessor[i].GetComponent<Node>().set_successor_moved(true);
			}
		}
		if(state == Node.node_state.FINAL)
			gameObject.renderer.material.SetColor("_Color", Color.red);
		else if(state == Node.node_state.INITIAL)
			gameObject.renderer.material.SetColor("_Color", Color.green);
		else
			gameObject.renderer.material.SetColor("_Color", Color.grey);
	}

	public Transform move_to_succ(Transform succ)
	{
		return successors.Find (x => x.GetComponent<Node> ().id == succ.GetComponent<Node> ().id);
	}

	public float get_f()
	{
		return h + g;
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
		Transform link;
		Transform arrow_head;

		link = Instantiate (edge_prefab, this.transform.position, Quaternion.identity) as Transform;
		link.GetComponent<LineRenderer> ().SetVertexCount (2);
		link.GetComponent<LineRenderer> ().SetPosition (0, this.transform.position);
		link.GetComponent<LineRenderer> ().SetPosition (1, n.transform.position);
		link.GetComponent<LineRenderer>().material = new Material (Shader.Find("Particles/Additive"));
		link.GetComponent<LineRenderer> ().SetColors (default_color, default_color);
		link.GetComponent<LineRenderer> ().SetWidth (0.1f, 0.1f);
		links.Add (link);

		Vector3 pos = n.transform.position - this.transform.position;
		pos [0] *= 0.8f;
		pos [1] *= 0.8f;
		pos += this.transform.position;



		arrow_head = Instantiate (edge_prefab, this.transform.position, Quaternion.identity) as Transform;
		arrow_head.GetComponent<LineRenderer> ().SetVertexCount (2);
		arrow_head.GetComponent<LineRenderer> ().SetPosition (0,pos);
		arrow_head.GetComponent<LineRenderer> ().SetPosition (1, n.transform.position);
		arrow_head.GetComponent<LineRenderer>().material = new Material (Shader.Find("Particles/Additive"));
		arrow_head.GetComponent<LineRenderer> ().SetColors (default_color, default_color);
		arrow_head.GetComponent<LineRenderer> ().SetWidth (0.4f, 0.1f);
		arrow_heads.Add (arrow_head);
	
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
	 * 'vel', d(Ri,Rf)
	 */
	public float time_processing(float pck_size) 
	{
		return (qtd_pck + 1) * pck_size / node_vel;
	}

	public List<Transform> get_succerssors()
	{
		return successors;
	}

	public void set_id(string id) 
	{
		this.id = id;
	}

	public void showBufferOnGUI () {
		Vector3 position = gameObject.transform.position.normalized;

		GUI.Label (new Rect (position.x,position.y,100,50), "label");
	}
}
