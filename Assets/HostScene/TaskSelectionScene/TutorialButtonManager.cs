using UnityEngine;
using System.Collections;

public class TutorialButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickHandler() {
		GameObject.Find ("ChallengeSelector").GetComponent<ChallengeSelector> ().SelectTutorial ();
	}
}
