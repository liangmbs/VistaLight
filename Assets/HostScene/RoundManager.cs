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
	public RecommendationSystem recommendataionSystem;
	public NotificationSystem notificationSystem;
	public MapLoader mapLoader;
	public MapController mapController;
	public IntroductionWindowController introductionWindowController;


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
		mapLoader.LoadMap ();
		StartSimulationPhase ();

		if (!SceneSetting.Instance.inTutorial) {
			ShowIntroductionWindow ();
		}

	}

	void Awake() {
		logger = GameObject.Find("BasicLoggerManager").GetComponent<VistaLightsLogger>();

		logger.StartRun ("run");
	}

	private void ShowIntroductionWindow() {
		introductionWindowController.gameObject.SetActive (true);
		introductionWindowController.UpdateText ();
		timeWidgetController.PauseGame ();
	}

	public void CloseIntroductionWindow() {
		introductionWindowController.gameObject.SetActive (false);

		StartSimulationPhase ();
	}
	
	// Update is called once per frame
	void Update () {
		// if (SceneSetting.Instance.inTutorial) {
		// 	return;
		// }

		if (networkScheduler.Scheduling){
			return;
		}

		if (phase == GamePhase.Simulation) {
			DateTime currentVirtualTime = timer.VirtualTime;
			if (currentVirtualTime >= SimulationPhaseStartTime + DecisionInterval) {
				StartDecisionPhase ();
			}
		} else if(phase == GamePhase.Decision) {
			DateTime currentDecisionTime = DateTime.Now;
			if (currentDecisionTime >= DecisionPhaseStartTime + DecisionTimeLimit) {
				TimeUp ();
			}
		}
	}

	public void StartDecisionPhase() {
		timeWidgetController.PauseGame ();

		recommendataionSystem.EnableRecommendationButton ();

		SubmitButton.SetActive (true);
		shipListController.ShowNewPriority ();

		DecisionPhaseStartTime = DateTime.Now;
		phase = GamePhase.Decision;

		logger.LogPhaseChange (GamePhase.Decision);
	}

	public void StartSimulationPhase() {
		timeWidgetController.SetSpeedOne ();

		recommendataionSystem.DisableRecommendationButton ();

		SubmitButton.SetActive (false);
		shipListController.HideNewPriority ();

		SimulationPhaseStartTime = timer.VirtualTime;
		phase = GamePhase.Simulation;

		logger.LogPhaseChange (GamePhase.Simulation);
	}

	public void SubmitAndContinueButtonClickHandler() {
		if (SceneSetting.Instance.GiveRecommendation) {
			if (!recommendataionSystem.recommendationRequested ||
			   !recommendataionSystem.isAllRecommendationsProcessed ()) {
				notificationSystem.Notify (NotificationType.Warning, 
					"Please request your recommendations and process them before submit.");
				return;
			}
		}
		SubmitAndContinue ();
	}



	public void SubmitAndContinue() {
		recommendataionSystem.ClearRecommendation ();

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

	private void TimeUp() {
		SubmitAndContinue ();
	}
}
