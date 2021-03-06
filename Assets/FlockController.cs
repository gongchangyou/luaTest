﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FlockController : MonoBehaviour {
	public float minVelocity = 1;
	public float maxVelocity = 8;
	public int flockSize = 2;

	public float centerWeight;
	public float velocityWeight;
	public float separationWeight;
	public float followWeight;
	public float randomizeWeight;

	public Flock prefab;
	public Transform target;

	internal Vector3 flockCenter;
	internal Vector3 flockVelocity;

	public ArrayList flockList = new ArrayList();

	// Use this for initialization
	void Start () {
		for (int i=0; i<flockSize; i++) {
			Flock flock = Instantiate(prefab, transform.position, transform.rotation) as Flock;
			flock.transform.parent = transform;
			flock.controller = this;
			flockList.Add(flock);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 center = Vector3.zero;
		Vector3 velocity = Vector3.zero;
		foreach (Flock flock in flockList) {
			center += flock.transform.position;
			velocity += flock.rigidbody.velocity;
		}

		flockCenter = center / flockSize;
		flockVelocity = velocity / flockSize;

	}

}
