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
	private List<ShipController> shipRecommended = new List<ShipController> ();

	public PriorityQueue priorityQueue;
	public Timer timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RequestRecommendation() {
		recommendationRequested = true;

		Recommend ();

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

		TimeSpan overdueThreshold = new TimeSpan (0, -12, 0);
		double maxNumPassengers = 0;
		ShipController shipToRecommend = null;
		foreach (ShipController ship in priorityQueue.queue) {
			if (!isShipInConsideration (ship)) {
				continue;
			}

			if (ship.Ship.Industry != IndustryType.Cruise) {
				continue;
			}

			TimeSpan overdueTime = timer.VirtualTime - ship.Ship.dueTime;
			if (overdueTime <= overdueThreshold) {
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
			recommendation.justification = "Because cruise ship overdue harms overall welfare.";

			ProvideRecommendation (recommendation);
		}
	}

	public void RecommendOverdueShip() {
		TimeSpan maxOverdueTime = TimeSpan.MinValue;
		ShipController shipToRecommend = null;
		foreach (ShipController ship in priorityQueue.queue) {
			
			if (!isShipInConsideration (ship))
				continue;
			
			TimeSpan overdueTime = timer.VirtualTime - ship.Ship.dueTime;
			if (overdueTime > maxOverdueTime) {
				maxOverdueTime = overdueTime;
				shipToRecommend = ship;
			}
		}

		TimeSpan recommendThreshold = new TimeSpan (0, -12, 0);
		if (maxOverdueTime > recommendThreshold) {
			Recommendation recommendation = new Recommendation ();
			recommendation.ship = shipToRecommend;
			recommendation.desiredPriority = 2;

			if (maxOverdueTime >= TimeSpan.Zero) {
				recommendation.justification = 
					"Because it is overdue, and overdue ship will have significant economic penalty.";
			} else {
				recommendation.justification = 
					"Because it will overdue soon, and overdue ship will have significant economic penalty.";
			}

			ProvideRecommendation (recommendation);
		}
	}

	public void RecommendHighValueShip() {
		while (recommendationProvided < 3) {

			double highestValue = 0;
			ShipController shipToRecommend = null;
			foreach (ShipController ship in priorityQueue.queue) {
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
				recommendation.justification = "Because this ship has very high cargo value.";

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
