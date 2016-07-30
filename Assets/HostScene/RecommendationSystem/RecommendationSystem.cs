using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class RecommendationSystem : MonoBehaviour {

	public List<RecommendationController> recommendations = new List<RecommendationController>(3);
	public Button requestRecommendationButton;
	public Text noRecommendationText;

	public bool recommendationRequested = false;
	private int recommendationProvided = 0;
	private bool showJustifiction = false;
	private List<ShipController> shipRecommended = new List<ShipController> ();

	public PriorityQueue priorityQueue;
	public Timer timer;
	public GameObject RecommendationTab;

	// Use this for initialization
	void Start () {
		if (!SceneSetting.Instance.GiveRecommendation) {
			RecommendationTab.SetActive (false);
		}

		showJustifiction = SceneSetting.Instance.RecommendWithJustification;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RequestRecommendation() {
		recommendationRequested = true;

		bool giveRecommendation = false;
		SceneSetting sceneSetting = GameObject.Find ("SceneSetting").GetComponent<SceneSetting> ();
		giveRecommendation = sceneSetting.GiveRecommendation;

		if (giveRecommendation) {
			Recommend ();
		}

		ShowRecommendations ();
	}

	public void Recommend() {
		RecommendCruiseShip ();
		RecommendOverdueShip ();
		RecommendHighValueShip ();
	}

	public void ClearRecommendation() {
		recommendationRequested = false;

		shipRecommended.Clear ();

		HideRecommendations ();
	}

	private void ShowRecommendations() {
		if (recommendationProvided == 0) {
			noRecommendationText.gameObject.SetActive (true);
		}

		for (int i = 0; i < recommendationProvided; i++) {
			recommendations [i].gameObject.SetActive (true);
		}

		requestRecommendationButton.gameObject.SetActive (false);
	}

	private void HideRecommendations() {
		recommendations[0].gameObject.SetActive (false);
		recommendations[1].gameObject.SetActive (false);
		recommendations[2].gameObject.SetActive (false);
		requestRecommendationButton.gameObject.SetActive (true);
		noRecommendationText.gameObject.SetActive (false);
		recommendationProvided = 0;
	}

	public void EnableRecommendationButton() {
		requestRecommendationButton.interactable = true;
	}

	public void DisableRecommendationButton() {
		requestRecommendationButton.interactable = false;
	}

	public void RecommendCruiseShip() {

		double maxNumPassengers = 0;
		ShipController shipToRecommend = null;
		foreach (ShipController ship in priorityQueue.Queue) {
			if (!isShipInConsideration (ship)) {
				continue;
			}

			if (ship.Ship.Industry != IndustryType.Cruise) {
				continue;
			}

			if (ship.Ship.dueTime <= ship.GetUnloadlingEta()) {
				continue;
			}

			if (ship.Ship.cargo > maxNumPassengers) {
				shipToRecommend = ship;
				maxNumPassengers = ship.Ship.cargo;
			}
		}

		// Create recommendation
		if (shipToRecommend != null) {
			Recommendation recommendation = new Recommendation ();
			recommendation.ship = shipToRecommend;
			recommendation.desiredPriority = 1;
			if (showJustifiction) {
				recommendation.justification = "Because cruise ship overdue harms overall welfare.";
			}

			ProvideRecommendation (recommendation);
		}
	}

	public void RecommendOverdueShip() {
		TimeSpan maxOverdueTime = TimeSpan.MinValue;
		ShipController shipToRecommend = null;
		foreach (ShipController ship in priorityQueue.Queue) {
			
			if (!isShipInConsideration (ship))
				continue;

			TimeSpan overdueTime = ship.GetUnloadlingEta () - ship.Ship.dueTime;
			if (overdueTime > TimeSpan.Zero && overdueTime > maxOverdueTime) {
				maxOverdueTime = overdueTime;
				shipToRecommend = ship;
			}
		}

		if (shipToRecommend != null) {
			Recommendation recommendation = new Recommendation ();
			recommendation.ship = shipToRecommend;
			recommendation.desiredPriority = 2;

			if (showJustifiction) {
				TimeSpan overdueTime = timer.VirtualTime - shipToRecommend.Ship.dueTime;
				if (overdueTime < TimeSpan.Zero) {
					recommendation.justification = 
					"Because it is overdue and has high economic penalty.";
				} else {
					recommendation.justification = 
					"Because it will overdue soon and will have high economic penalty.";
				}
			}

			ProvideRecommendation (recommendation);
		}
	}

	public void RecommendHighValueShip() {
		while (recommendationProvided < 3) {

			double highestValue = 0;
			ShipController shipToRecommend = null;
			foreach (ShipController ship in priorityQueue.Queue) {
				if (!isShipInConsideration (ship))
					continue;

				if (ship.Ship.cargo * ship.Ship.value > highestValue) {
					highestValue = ship.Ship.cargo * ship.Ship.value;
					shipToRecommend = ship;
				}
			}

			if (shipToRecommend != null) {
				Recommendation recommendation = new Recommendation ();
				recommendation.ship = shipToRecommend;
				recommendation.desiredPriority = 3;
				if (showJustifiction) {
					recommendation.justification = "Because this ship has very high cargo value.";
				}

				ProvideRecommendation (recommendation);
			} else {
				break;
			}
		}


	}

	private void ProvideRecommendation(Recommendation recommendation) {
		if (recommendationProvided >= 3) {
			Debug.LogError ("No more slot for more recommendation");
			return;
		}
		shipRecommended.Add (recommendation.ship);
		recommendations [recommendationProvided].SetRecommendation (recommendation);
		recommendationProvided++;
	}

	public bool isShipInConsideration(ShipController ship) {
		if (shipRecommended.Contains (ship))
			return false;

		if (ship.status != ShipStatus.Waiting)
			return false;

		if (ship.Ship.cargo == 0)
			return false;

		return true;
	}

	public bool isAllRecommendationsProcessed() {
		for (int i = 0; i < recommendationProvided; i++) {
			if (recommendations [i].isProcessed == false) {
				return false;
			}
		}
		return true;
	}
}
