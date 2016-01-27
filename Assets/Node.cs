using UnityEngine;
using System.Collections;
using System;
public class Node : IComparable {
	public float nodeTotalCost;
	public float estimateCost;
	public bool bObstacle;
	public Node parent;
	public Vector3 position;

	public Node(){
		this.estimateCost = 0.0f;
		this.nodeTotalCost = 1.0f;
		this.bObstacle = false;
		this.parent = null;
	}

	public Node(Vector3 pos){
		this.estimateCost = 0.0f;
		this.nodeTotalCost = 1.0f;
		this.bObstacle = false;
		this.parent = null;
		this.position = pos;
	}

	public void MarkAsObstacle(){
		this.bObstacle = true;
	}

	public int CompareTo(object obj){
		Node node = obj as Node;
		if (this.estimateCost < node.estimateCost)
			return -1;
		if (this.estimateCost > node.estimateCost)
			return 1;
		return 0;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
