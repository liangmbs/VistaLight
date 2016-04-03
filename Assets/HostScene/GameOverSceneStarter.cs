using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverSceneStarter : MonoBehaviour {

	public PriorityQueue priorityQueue;
	public PriorityQueue waitList;

	public BudgetCounter budgetCounter;
	public WelfareCounter welfareCounter;
	public Timer timer;
	public MapController mapController;
	public MapEventProcessor mapEventProcessor;
	public DockUtilizationCounter dockUtilizationCounter;

	public VistaLightsLogger logger;


	// Use this for initialization
	void Start () {
	
	}

	void Awake() {
		logger = GameObject.Find("BasicLoggerManager").GetComponent<VistaLightsLogger>();	
	}
	
	// Update is called once per frame
	void Update () {
		bool shouldGameEnd = false;
		if (IsEndTimeReached ()) {
			shouldGameEnd = true;
		} else if (IsAllEventProcessed ()) {
			shouldGameEnd = true;
		} else if (IsWelfareZero ()) {
			shouldGameEnd = true;
		}

		if (shouldGameEnd) {
			LoadGameOverScene ();
		}

	}

	void LoadGameOverScene() {
		timer.Speed = 0;
		budgetCounter.freeze = true;
		welfareCounter.freeze = true;

		logger.EndRun (budgetCounter.money, welfareCounter.Welfare, 
			dockUtilizationCounter.CalculateAverageUtilization());

		SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

	private bool IsEndTimeReached() {
		DateTime currentTime = timer.VirtualTime;
		DateTime gameEndTime = mapController.Map.EndTime;
		return currentTime >= gameEndTime;
	}

	private bool IsAllEventProcessed() {
		// FIXME(Yifan): Check if oil is cleaned here
		if (mapEventProcessor.MapEvents.Count == 0 && 
			priorityQueue.GetCount () == 0 && 
			waitList.GetCount () == 0) {
			return true;
		} 
		return false;
	}

	private bool IsWelfareZero() {
		return Math.Abs (welfareCounter.Welfare) < 1e-3;
	}
}
