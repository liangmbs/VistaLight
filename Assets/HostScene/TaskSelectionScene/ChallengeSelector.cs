﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ChallengeSelector : MonoBehaviour {
	public SceneSetting sceneSetting;
	public bool tutorialPlayed = false;
	public bool challenge1Played = false;
	public bool challenge2Played = false;
	public bool challenge3Played = false;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		DestoryIfInstanceExist ();
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
		sceneSetting.MapName = "houston_game_1";
		tutorialPlayed = true;
		StartGame ();
	}

	public void SelectChallenge1() {
		sceneSetting.MapName = "houston_game_1";
		sceneSetting.GiveRecommendation = false;
		challenge1Played = true;
		StartGame ();
	}

	public void SelectChallenge2() {
		sceneSetting.MapName = "houston_game_1";
		sceneSetting.GiveRecommendation = true;
		sceneSetting.RecommendWithJustification = false;
		challenge2Played = true;
		StartGame ();
	}

	public void SelectChallenge3() {
		sceneSetting.MapName = "houston_game_1";
		sceneSetting.GiveRecommendation = true;
		sceneSetting.RecommendWithJustification = true;
		challenge3Played = true;
		StartGame ();
	}

	private void StartGame() {
		SceneManager.LoadScene("HostGameScene", LoadSceneMode.Single);
	}
}