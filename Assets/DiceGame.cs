using UnityEngine;
using System.Collections;

public class DiceGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var v1 = new Vector3 (1,0,0);
		var v2 = new Vector3 (1,1,0);
		var angle = Vector3.Angle (v1, v2);
		Debug.Log ("angle = " + angle);

		StartCoroutine (UpdateRandom());
	}

	IEnumerator UpdateRandom(){
		while (true) {
			var force = Random.insideUnitSphere * 2;
			Debug.DrawLine (new Vector3 (0, 0, 0), force, Color.red);
			yield return new WaitForSeconds(1 + Random.Range(-0.5f, 0.5f));
		}

	}
	void OnDrawGizmos(){
		Debug.DrawLine (new Vector3 (0, 0, 0), new Vector3 (1, 0, 0), Color.red);
		Debug.DrawLine (new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), Color.red);
	
		Gizmos.color = new Color (1,0,0,0.5f);
//		Gizmos.DrawCube (Vector3.zero, new Vector3 (1, 2, 3));

	}

	// Update is called once per frame
	void Update () {
	
	}
	[SerializeField]
	private GUIText guiText;

	public string inputValue = "1";
	void OnGUI(){
		GUI.Label(new Rect(10,10,100,20), "Input:");
		inputValue = GUI.TextField (new Rect(120, 10, 50, 20), inputValue, 25);	
		if (GUI.Button (new Rect (100, 50, 50, 30), "Play")) {
			Debug.Log ("Throwing dice...");
			Debug.Log ("Finding random between 1 to 6...");
//			int diceResult = Random.Range(1,7);
			int diceResult = throwLoadedDice();

			Debug.Log ("Result: " + diceResult);
			if(diceResult == int.Parse(inputValue)){
				guiText.text = "Dice RESULT: " + diceResult.ToString() + " YOU WIN!";

			}else{
				guiText.text = "Dice RESULT: " + diceResult.ToString() + "\r\n YOU LOSE!";

			}
		}

	}

	int throwLoadedDice(){
		int randomProbability = Random.Range (1, 101);
		if (randomProbability < 36) {
			return 6;
		} else {
			return Random.Range (1, 6);
		}

	}
}
