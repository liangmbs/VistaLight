using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Challenge1ButtonController : MonoBehaviour {

	public ChallengeSelector challengeSelector;
	public Button challenge1Button;

	void Awake() {
		challengeSelector = GameObject.Find ("ChallengeSelector").GetComponent<ChallengeSelector> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (challengeSelector.tutorialPlayed && !challengeSelector.challenge1Played) {
			challenge1Button.interactable = true;	
		} else {
			challenge1Button.interactable = false;	
		}
	}

	public void ClickHandler() {
		GameObject.Find ("ChallengeSelector").GetComponent<ChallengeSelector> ().SelectChallenge1();
	}
}
