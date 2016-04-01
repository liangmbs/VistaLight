using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Challenge3ButtonController : MonoBehaviour {

	public ChallengeSelector challengeSelector;
	public Button challenge3Button;

	void Awake() {
		challengeSelector = GameObject.Find ("ChallengeSelector").GetComponent<ChallengeSelector> ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (challengeSelector.challenge2Played && !challengeSelector.challenge3Played) {
			challenge3Button.interactable = true;	
		} else {
			challenge3Button.interactable = false;	
		}
	}

	public void ClickHandler() {
		GameObject.Find ("ChallengeSelector").GetComponent<ChallengeSelector> ().SelectChallenge3();
	}
}
