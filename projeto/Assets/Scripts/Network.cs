using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Network : MonoBehaviour {

	public    const     float        node_vel    =  40.0f;  // 40kbs
	public    const     float        pck_size    =  100.0f; // each packge has 100kbits of data
	private     	 	List<Node>   state_space;
	private  			List<Node>   explored_set;
	private             List<Node>   frontier_set;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
}
