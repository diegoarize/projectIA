using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	private    int  	          dist_to_final; 
	private    int                qtd_pck;
	private    List<Node>         successors;
	private    float              link_vel;// velocity in kbits/seg
	public     enum node_state    {INITIAL, FINAL, INTERMEDIARY};
	private    node_state         state  =  node_state.INITIAL;  


	public void set_state(node_state s) 
	{
		state = s;
	}


	public void set_dist(int dist)
	{
		dist_to_final = dist;
	}


	public void set_link_vel(float vel) 
	{
		link_vel  =  vel;
	}


	public void insert_sucessor(Node n)
	{
		successors.Add (n);
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

}
