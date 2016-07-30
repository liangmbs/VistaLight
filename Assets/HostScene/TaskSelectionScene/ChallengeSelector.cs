using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChallengeSelector : MonoBehaviour {
	public bool tutorialPlayed = false;
	public Button TutorialButton;

	public bool challenge1Played = false;
	public Button Challenge1Button;

	public bool challenge2Played = false;
	public Button Challenge2Button;

	public bool challenge3Played = false;
	public Button Challenge3Button;

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
		// Init SceneSetting
		SceneSetting _ = SceneSetting.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		// Tutorial always interactable
		TutorialButton.interactable = true;
		Challenge1Button.interactable = tutorialPlayed && !challenge1Played;
		Challenge2Button.interactable = challenge1Played && !challenge2Played;
		Challenge3Button.interactable = challenge2Played && !challenge3Played;
	}


	public void SelectTutorial() {
		if (SceneSetting.Instance.IsMaster) {
			GetComponent<PhotonView>().RPC ("PlayTutorial", PhotonTargets.All);
		}
	}

	[PunRPC]
	public void PlayTutorial() {
		SceneSetting.Instance.MapName = "houston_game_0";
		SceneSetting.Instance.inTutorial = true;
		tutorialPlayed = true;
		StartGame ();
	}

	[PunRPC]
	public void SelectChallenge1() {
		if (SceneSetting.Instance.IsMaster) {
			GetComponent<PhotonView>().RPC ("PlayTutorial", PhotonTargets.All);
		}
	}

	public void PlayChallenge1() {
		SceneSetting.Instance.MapName = "houston_game_1";
		SceneSetting.Instance.GiveRecommendation = false;
		SceneSetting.Instance.inTutorial = false;
		challenge1Played = true;
		StartGame ();
	}

	public void SelectChallenge2() {
		if (SceneSetting.Instance.IsMaster) {
			GetComponent<PhotonView>().RPC ("PlayTutorial", PhotonTargets.All);
		}
	}

	[PunRPC]
	public void PlayChallenge2() {
		SceneSetting.Instance.MapName = "houston_game_2";
		//SceneSetting.Instance.GiveRecommendation = true;
		//SceneSetting.Instance.RecommendWithJustification = false;
		SceneSetting.Instance.inTutorial = false;
		challenge2Played = true;

		StartGame ();
	}


	public void SelectChallenge3() {
		if (SceneSetting.Instance.IsMaster) {
			GetComponent<PhotonView>().RPC ("PlayTutorial", PhotonTargets.All);
		}
	}

	[PunRPC]
	public void PlayChallenge3() {
		SceneSetting.Instance.MapName = "houston_game_3";
		//sceneSetting.GiveRecommendation = true;
		//sceneSetting.RecommendWithJustification = true;
		SceneSetting.Instance.inTutorial = false;
		challenge3Played = true;
		StartGame ();
	}

	private void StartGame() {
		PhotonNetwork.LoadLevel("HostGameScene");
	}
}
