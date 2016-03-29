using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ChallengeSelector : MonoBehaviour {
	public SceneSetting sceneSetting;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SelectChallenge1() {	
		sceneSetting.MapName = "houston_game_1";
		StartGame ();
	}

	public void SelectChallenge2() {
		sceneSetting.MapName = "houston_game_1";
		sceneSetting.GiveRecommendation = false;
		StartGame ();
	}

	public void SelectChallenge3() {
		sceneSetting.MapName = "houston_game_1";
		sceneSetting.GiveRecommendation = true;
		sceneSetting.RecommendWithJustification = false;
		StartGame ();
	}

	public void SelectChallenge4() {
		sceneSetting.MapName = "houston_game_1";
		sceneSetting.GiveRecommendation = true;
		sceneSetting.RecommendWithJustification = true;
		StartGame ();
	}

	private void StartGame() {
		SceneManager.LoadScene("HostGameScene", LoadSceneMode.Single);
	}
}
