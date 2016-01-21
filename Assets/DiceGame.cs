using UnityEngine;
using System.Collections;

public class DiceGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
			int diceResult = Random.Range(1,7);
			Debug.Log ("Result: " + diceResult);
			if(diceResult == int.Parse(inputValue)){
				guiText.text = "Dice RESULT: " + diceResult.ToString() + " YOU WIN!";

			}else{
				guiText.text = "Dice RESULT: " + diceResult.ToString() + "\r\n YOU LOSE!";

			}
		}

	}
}
