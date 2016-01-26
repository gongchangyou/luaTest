using UnityEngine;
using System.Collections;

public class VehicleFollowing : MonoBehaviour {
	public PathTest path;
	public float speed = 20.0f;
	public float mass = 5.0f;
	public bool isLooping = true;

	private float curSpeed;
	private int curPathIndex;
	private float pathLength;
	private Vector3 targetPoint;
	Vector3 velocity;
	// Use this for initialization
	void Start () {
		pathLength = path.Length;
		curPathIndex = 0;
		velocity = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		curSpeed = speed * Time.deltaTime;

		targetPoint = path.GetPoint (curPathIndex);

		if (Vector3.Distance (transform.position, targetPoint) < path.Radius) {
			if(curPathIndex < pathLength - 1) curPathIndex++;
			else if (isLooping) curPathIndex = 0;
			else return;
		}

		if (curPathIndex >= pathLength) {
			Debug.Log("impossible curPathIndex" + curPathIndex);
			return;
		}

		if (curPathIndex >= pathLength - 1 && !isLooping) {
			velocity += Steer (targetPoint, true);
		} else {
			velocity += Steer(targetPoint);
		}

		Debug.DrawLine (transform.position, transform.position+velocity * 10, Color.green);
		transform.position += velocity;
//		transform.LookAt (velocity + transform.position);
		transform.rotation = Quaternion.LookRotation (velocity);

	}

	public Vector3 Steer(Vector3 target, bool bFinalPoint = false){
		Vector3 desiredVelocity = target - transform.position;
		float dist = desiredVelocity.magnitude;
		desiredVelocity.Normalize ();
		if (bFinalPoint && dist < 10.0f)
			desiredVelocity *= curSpeed * dist / 10.0f;//减速
		else
			desiredVelocity *= curSpeed;
		Vector3 steeringForce = desiredVelocity - velocity;
		Vector3 acceleration = steeringForce / mass;
		return acceleration;
	}
}
