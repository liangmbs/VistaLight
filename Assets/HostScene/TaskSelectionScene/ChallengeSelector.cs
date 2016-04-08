using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ChallengeSelector : MonoBehaviour {
	public SceneSetting sceneSetting;
	public bool tutorialPlayed = false;
	public bool challenge1Played = false;
	public bool challenge2Played = false;
	public bool challenge3Played = false;

	public VistaLightsLogger logger;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		DestoryIfInstanceExist ();

		logger = GameObject.Find ("BasicLoggerManager").GetComponent<VistaLightsLogger> ();
	}

	private static ChallengeSelector instance = null;
	void DestoryIfInstanceExist() {
		if (instance != null) {
			Destroy (gameObject); 
			return;
		}
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SelectTutorial() {	
		sceneSetting.MapName = "houston_game_0";
		sceneSetting.inTutorial = true;
		tutorialPlayed = true;
		StartGame ();
	}

	public void SelectChallenge1() {
		sceneSetting.MapName = "houston_game_1";
		sceneSetting.GiveRecommendation = false;
		sceneSetting.inTutorial = false;
		challenge1Played = true;
		StartGame ();
	}

	public void SelectChallenge2() {
		sceneSetting.MapName = "houston_game_2";
		//sceneSetting.GiveRecommendation = true;
		//sceneSetting.RecommendWithJustification = false;
		sceneSetting.inTutorial = false;
		challenge2Played = true;

		StartGame ();
	}

	public void SelectChallenge3() {
		sceneSetting.MapName = "houston_game_3";
		// sceneSetting.GiveRecommendation = true;
		// sceneSetting.RecommendWithJustification = true;
		sceneSetting.inTutorial = false;
		challenge3Played = true;
		StartGame ();
	}

	private void StartGame() {
		SceneManager.LoadScene("HostGameScene", LoadSceneMode.Single);
	}
}
