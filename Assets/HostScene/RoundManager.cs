using UnityEngine;
using System;
using System.Collections;

public enum GamePhase{ 
	Simulation, Decision
}

public class RoundManager : MonoBehaviour {

	public GamePhase phase = GamePhase.Simulation;

	public GameObject SubmitButton;
	public TimeWidgetController timeWidgetController;
	public Timer timer;
	public ShipListController shipListController;
	public NetworkScheduler networkScheduler;

	public DateTime SimulationPhaseStartTime;
	public TimeSpan DecisionInterval = new TimeSpan(12, 0, 0);

	public DateTime DecisionPhaseStartTime;
	public TimeSpan DecisionTimeLimit = new TimeSpan(0, 2, 0);

	// Use this for initialization
	void Start () {
		SimulationPhaseStartTime = timer.VirtualTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (phase == GamePhase.Simulation) {
			DateTime currentVirtualTime = timer.VirtualTime;
			if (currentVirtualTime >= SimulationPhaseStartTime + DecisionInterval) {
				StartDecisionPhase ();
			}
		} else if(phase == GamePhase.Decision) {
			DateTime currentDecisionTime = DateTime.Now;
			if (currentDecisionTime >= DecisionPhaseStartTime + DecisionTimeLimit) {
				StartSimulationPhase ();
			}
		}
	}

	public void StartDecisionPhase() {
		timeWidgetController.PauseGame ();

		SubmitButton.SetActive (true);
		shipListController.ShowNewPriority ();

		DecisionPhaseStartTime = DateTime.Now;
		phase = GamePhase.Decision;
	}

	public void StartSimulationPhase() {
		timeWidgetController.SetSpeedOne ();

		SubmitButton.SetActive (false);
		shipListController.HideNewPriority ();

		SimulationPhaseStartTime = timer.VirtualTime;
		phase = GamePhase.Simulation;
	}



	public void SubmitAndContinue() {
		Submit ();
		StartSimulationPhase ();
	}

	private void Submit() {
		networkScheduler.RequestReschedule ();
	}
}
