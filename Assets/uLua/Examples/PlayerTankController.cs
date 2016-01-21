using UnityEngine;
using System.Collections;

public class PlayerTankController : MonoBehaviour
{
	private float curSpeed, targetSpeed, rotSpeed;
	private float maxForwardSpeed = 3f;
	private float maxBackwardSpeed = -3f;
	[SerializeField]
	private GameObject Turret;
	// Use this for initialization
	void Start ()
	{
		rotSpeed = 150.0f;
		curSpeed = 0f;

		float dist = Vector3.Distance (new Vector3(1,0,2), new Vector3(-1,0,-2));
		Debug.Log ("dist = "+ dist);
		var rand = Random.Range (-10, 10);
		Debug.Log ("rand = "+ rand);
	}

	void UpdateControl(){
		var playerPlane = new Plane (Vector3.up, transform.position + new Vector3 (0, 0, 0));
		Ray RayCast = Camera.main.ScreenPointToRay (Input.mousePosition);
		float HitDist = 0;
		if (playerPlane.Raycast (RayCast, out HitDist)) {
			
			Vector3 RayHitPoint = RayCast.GetPoint(HitDist);

			Quaternion targetRotation = Quaternion.LookRotation(RayHitPoint - transform.position);

			Turret.transform.rotation = Quaternion.Slerp(Turret.transform.rotation, targetRotation, Time.deltaTime * 10f);
		}

		if (Input.GetKey (KeyCode.W)) {
			targetSpeed = maxForwardSpeed;
		} else if (Input.GetKey (KeyCode.S)) {
			targetSpeed = maxBackwardSpeed;
		} else if (Input.GetKey (KeyCode.A)) {
			transform.Rotate(0, -rotSpeed * Time.deltaTime, 0);
		}else if (Input.GetKey (KeyCode.D)) {
			transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
		}

		curSpeed = Mathf.Lerp (curSpeed, targetSpeed, 7.0f * Time.deltaTime);
		transform.Translate (Vector3.forward * Time.deltaTime * curSpeed);

	}

	// Update is called once per frame
	void Update ()
	{
		UpdateControl ();
	}
}

