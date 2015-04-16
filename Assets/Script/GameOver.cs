using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class GameOver : MonoBehaviour {

	public Text score;

	void Start(){
		score = GameObject.Find ("/Canvas/Score").GetComponent<Text> ();
		float money = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ().score;
		score.text = money.ToString ();
	}
}
