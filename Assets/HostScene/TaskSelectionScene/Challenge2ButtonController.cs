using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Challenge2ButtonController : MonoBehaviour {

	public ChallengeSelector challengeSelector;
	public Button challenge2Button;

	void Awake() {
		challengeSelector = GameObject.Find ("ChallengeSelector").GetComponent<ChallengeSelector> ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (challengeSelector.challenge1Played && !challengeSelector.challenge2Played) {
			challenge2Button.interactable = true;	
		} else {
			challenge2Button.interactable = false;	
		}
	}

	public void ClickHandler() {
		GameObject.Find ("ChallengeSelector").GetComponent<ChallengeSelector> ().SelectChallenge2();
	}
}
