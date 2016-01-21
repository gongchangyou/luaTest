using UnityEngine;
using System.Collections;

public class SlotMachine : MonoBehaviour {
	private bool startPin = false;
	private float elapsedTime = 0.0f;

	private float spinDuration1 = 1.0f;
	private float spinDuration2 = 2.0f;
	private float spinDuration3 = 3.0f;

	private int reel1Result = 0;
	private int reel2Result = 0;
	private int reel3Result = 0;

	[SerializeField]
	private GameObject reel1;
	[SerializeField]
	private GameObject reel2;
	[SerializeField]
	private GameObject reel3;

	int numberOfSym = 10;
	int credits = 1000;
	int betAmount = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.Label(new Rect(150, 40,100,20), "Your bet:");
		betAmount = int.Parse(GUI.TextField(new Rect(220,40,50,20), betAmount.ToString(), 25));
		GUI.Label (new Rect (300, 40, 100, 20), "Credits: " + credits.ToString ());
		if (GUI.Button (new Rect (280, 300, 150, 40), "Pull Liver")) {
			if(betAmount <= 0){
				guiText.text = "can not bet zero";
			}else if(betAmount > credits){
				guiText.text = "credits not enough!";
			}else{
				startPin = true;
			}
		}

	}
	void checkResult(){
		Debug.Log ("reel1=" + reel1Result.ToString () + "reel2=" + reel2Result.ToString () + "reel3=" + reel3Result);
		if (reel1Result == reel2Result && reel2Result == reel3Result) {
			guiText.text = "JACKPOT YOU WIN!!!";
			credits += betAmount * 50;
			return;
		}
		if (reel1Result == 0 && reel3Result == 0) {
			guiText.text = "YOU WIN " + (betAmount / 2).ToString();
			credits -= betAmount / 2;
			return;
		}

		if (reel1Result == reel3Result) {
			guiText.text = "YOU WIN "+ (betAmount * 2).ToString();
			credits += betAmount * 2;
			return;
		}

		if (reel1Result == reel2Result) {
			guiText.text = "AWW... ALMOST JACKPOT! ";
			return;
		}

		credits -= betAmount;
		guiText.text = "YOU LOSE!!!";

	
	}
	void FixedUpdate(){
		if (!startPin)
			return;
		elapsedTime += Time.deltaTime;

		if (elapsedTime < spinDuration1) {
			reel1Result = zeroProbability();
			reel1.guiText.text = reel1Result.ToString();
		}

		if (elapsedTime < spinDuration2) {
			reel2Result = Random.Range (0, numberOfSym);
			reel2.guiText.text = reel2Result.ToString();
		}

		if (elapsedTime < spinDuration3) {
			reel3Result = zeroProbability();

			//差点中奖
			if(reel1Result == reel2Result && reel3Result != reel1Result){
				reel3Result = reel3Result > reel1Result ? reel1Result + 1 : reel1Result - 1;
				reel3Result %= numberOfSym;
			}

			reel3.guiText.text = reel3Result.ToString ();

		} else {
			startPin = false;
			elapsedTime = 0.0f;
			checkResult();
		}

	}

	int zeroProbability(){
		int rand = Random.Range (1, 101);
		if (rand < 31) {
			return 0;
		} else {
			return Random.Range(1, numberOfSym);
		}

	}
}
