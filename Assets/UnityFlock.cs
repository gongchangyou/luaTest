using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class UnityFlock : MonoBehaviour {
	public float minSpeed = 20.0f;
	public float turnSpeed = 20.0f;
	public float randomFreq = 20.0f;
	public float randomForce = 20.0f;

	public float toOriginForce = 50.0f;
	public float toOriginRange = 100.0f;

	public float gravity = 2.0f;

	public float avoidanceRadius = 50.0f;
	public float avoidanceForce = 20.0f;

	public float followVelocity = 4.0f;
	public float followRadius = 40.0f;

	private Transform origin;
	private Vector3 velocity = Vector3.zero;
	private Vector3 normalizedVelocity;
	private Vector3 randomPush;
	private Vector3 originPush;
	private Transform[] objects;
	private UnityFlock[] otherFlocks;
	private Transform transformComponent;
	// Use this for initialization
	void Start () {
		randomFreq = 1.0f / randomFreq;



		transformComponent = transform;


		//why set parent = null??
		transform.parent = null;

		StartCoroutine (UpdateRandom ());
	}
	void Awake(){
		origin = transform.parent;
		UnityFlock[] tempFlocks = null;
		
		if (origin) {
			tempFlocks = origin.GetComponentsInChildren<UnityFlock>();
		}
		Debug.Log ("tempFlocks=" + tempFlocks.Length + "name=" + name);
		objects = new Transform[tempFlocks.Length];
		otherFlocks = new UnityFlock[tempFlocks.Length];
		
		for (int i = 0; i<tempFlocks.Length; i++) {
			objects[i] = tempFlocks[i].transform;
			otherFlocks[i] = tempFlocks[i];
		}


	}
	IEnumerator UpdateRandom(){
		while (true) {
			randomPush = Random.insideUnitSphere * randomForce;
			yield return new WaitForSeconds(randomFreq + Random.Range(-randomFreq / 2.0f, randomFreq / 2.0f));
		}

	}

	Vector3 start = new Vector3(0,2,0);
	Vector3 test = new Vector3(0,2,0);
	float theta;
	// Update is called once per frame
	void Update () {
		theta += Time.deltaTime;
		//Slerp 是如何确定球的半径的？
//		test = Vector3.Slerp (test, new Vector3(1,0,0), Time.deltaTime);
//
//		Debug.DrawLine (new Vector3 (0, 0, 0), test, Color.red);
//
//
//		Debug.Log ("test=" + test );//.x + " y=" + test.y + " z=" + test.z);
//		========================
		Vector3 avgVelocity = Vector3.zero;
		Vector3 avgPosition = Vector3.zero;
		float speed = velocity.magnitude;
		float count = 0;
		float f = 0.0f;
		float d = 0.0f;
		Vector3 myPosition = transformComponent.position;
		Vector3 forceV;
		Vector3 toAvg;
		Vector3 wantedVel;
//		Debug.Log ("objects=" + objects.Length);

		for (int i=0; i<objects.Length; i++) {
			Transform transform = objects [i];
			if (transform != transformComponent) {
				Vector3 otherPosition = transform.position;
				avgPosition += otherPosition;
				count ++;

				forceV = myPosition - otherPosition;
				d = forceV.magnitude;

				if (d < followRadius) {
					if (d < avoidanceRadius) {
						f = 1.0f - (d / avoidanceRadius);//判断靠近程度 f越小表示距离越远
						if (d > 0)
							avgVelocity += forceV.normalized * f * avoidanceForce;//距离越近弹开的越快
					}

					//计算出靠太近的大伙的平均速度
					f = d / followRadius;
					var otherSealgull = otherFlocks [i];
					avgVelocity += otherSealgull.normalizedVelocity * f * followVelocity;//近的权重高些 远的权重低
				}
			}
		}
				if(count > 0){
					avgVelocity /= count;
					toAvg = avgPosition / count - myPosition;//目标位置
				}else{
					toAvg = Vector3.zero;
				}
				//跟着leader走
				forceV = origin.position - myPosition;
				d = forceV.magnitude;
				f = d / toOriginRange;
				if(d > 0)
					originPush = forceV.normalized * f * toOriginForce;
				
				if(speed < minSpeed && speed > 0){
//					Debug.Log ("name = " + name + "velocity =" + velocity + " speed=" + speed + " minSpeed=" + minSpeed);
					velocity = velocity / speed * minSpeed;//将速度拉至最小速度
				}

				wantedVel = velocity;
				
				wantedVel -= wantedVel * Time.deltaTime;//减速
				
				wantedVel += randomPush * Time.deltaTime;//随机方向 20以内的向量
				
				wantedVel +=  originPush * Time.deltaTime;//跟着leader走
			
				wantedVel += avgVelocity * Time.deltaTime;//靠太近导致弹开的力 20以内
			
				wantedVel += toAvg.normalized * gravity * Time.deltaTime;//平均位置
			

				velocity = Vector3.RotateTowards(velocity, wantedVel, turnSpeed * Time.deltaTime, 100.0f);

				transformComponent.rotation = Quaternion.LookRotation(velocity);
				transformComponent.Translate(velocity * Time.deltaTime, Space.World);
				normalizedVelocity = velocity.normalized;
			

	}
}
