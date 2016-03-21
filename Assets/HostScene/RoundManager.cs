using UnityEngine;
using UnityEngine.UI;
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
	public VistaLightsLogger logger;

	public Toggle burningToggle;
	public Toggle dispersantToggle;
	public Toggle skimmerToggle;
	public OilSpillingAction oilCleaningAction;

	public DateTime SimulationPhaseStartTime;
	public TimeSpan DecisionInterval = new TimeSpan(6, 0, 0);

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
				SubmitAndContinue ();
			}
		}
	}

	public void StartDecisionPhase() {
		timeWidgetController.PauseGame ();

		SubmitButton.SetActive (true);
		shipListController.ShowNewPriority ();

		DecisionPhaseStartTime = DateTime.Now;
		phase = GamePhase.Decision;

		logger.LogPhaseChange (GamePhase.Decision);
	}

	public void StartSimulationPhase() {
		timeWidgetController.SetSpeedOne ();

		SubmitButton.SetActive (false);
		shipListController.HideNewPriority ();

		SimulationPhaseStartTime = timer.VirtualTime;
		phase = GamePhase.Simulation;

		logger.LogPhaseChange (GamePhase.Simulation);
	}



	public void SubmitAndContinue() {
		Submit ();
		StartSimulationPhase ();
	}

	private void Submit() {
		networkScheduler.RequestReschedule ();

		if (burningToggle.isOn) {
			burningToggle.isOn = false;
			oilCleaningAction.Burn ();
		} else if (dispersantToggle.isOn) {
			dispersantToggle.isOn = false;
			oilCleaningAction.Dispersant ();
		} else if (skimmerToggle.isOn) {
			skimmerToggle.isOn = false;
			oilCleaningAction.Skimmers ();
		}

		logger.LogSubmitButton ();
	}
}
