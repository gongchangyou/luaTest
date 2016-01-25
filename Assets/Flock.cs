using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flock : MonoBehaviour {
	internal FlockController controller;
	private Transform transformComponent;
	public float turnSpeed = 20.0f;
	// Use this for initialization
	void Start () {
		transformComponent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (controller) {
			Vector3 relativePos = steer ();// * Time.deltaTime;
			if(relativePos != Vector3.zero){
				rigidbody.velocity = Vector3.Slerp (rigidbody.velocity, relativePos, Time.deltaTime);
		
			}else{
				Debug.Log("velocity=0");
			}

			var velocity = rigidbody.velocity;
//			float speed = rigidbody.velocity.magnitude;
//			if(speed > controller.maxVelocity){
//				rigidbody.velocity = rigidbody.velocity.normalized * controller.maxVelocity;
//			}else if(speed < controller.minVelocity){
//				rigidbody.velocity = rigidbody.velocity.normalized * controller.minVelocity;
//
//			}

		}
	} 
	Vector3 avgVelocity = Vector3.zero;
	public float avoidanceRadius = 50.0f;
	public float followRadius = 40.0f;
	public float avoidanceForce = 5.0f;
	public float followVelocity = 4.0f;
	private Vector3 steer(){
		Vector3 center = controller.flockCenter - transform.position;

		Vector3 velocity = controller.flockVelocity - rigidbody.velocity;

		Vector3 follow = controller.target.position - transform.position;

		Vector3 separation = Vector3.zero;
		float f = 0.0f;
		float d = 0.0f;
		foreach (Flock flock in controller.flockList) {
			if(flock != this){
				Vector3 relativePos = transform.position - flock.transform.position;

				separation += relativePos / relativePos.sqrMagnitude;


			}
		}
		Vector3 randomize = new Vector3( Random.Range (-3f, 3f), Random.Range (-3f, 3f), Random.Range (-3f, 3f));

		//randomize.Normalize ();
//		Debug.Log ("center = " + center);
//		Debug.Log ("velocity = " + velocity);
//		Debug.Log ("follow = " + follow);
//		Debug.Log ("separation = " + separation);
//		Debug.Log ("randomize = " + randomize);
		var result = controller.centerWeight * center +
			controller.velocityWeight * velocity +
			controller.followWeight * follow +
			controller.separationWeight * separation +
			controller.randomizeWeight * randomize;
//		Debug.Log ("velocity = " + velocity);
		Debug.DrawLine (transformComponent.position, controller.flockCenter, Color.green);
		Debug.DrawLine (transformComponent.position, transform.position + separation, Color.red);
		Debug.DrawLine (transformComponent.position, transformComponent.position + randomize, Color.blue);
//		Debug.DrawLine (transformComponent.position, transform.position + separation, Color.yellow);


		return result;
	}
}
