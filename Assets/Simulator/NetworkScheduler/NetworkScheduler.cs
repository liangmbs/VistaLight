using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

public class NetworkScheduler : MonoBehaviour {

	private bool rescheduleRequested = false;

	public PriorityQueue priorityQueue;
	public PriorityQueue waitList;
	public bool Scheduling = false;

	public GameObject schedulingMask;
	public Slider progressBar;

	public VistaLightsLogger logger;

	public void RequestReschedule() {
		rescheduleRequested = true;	
	}

	void Awake() {
		logger = GameObject.Find("BasicLoggerManager").GetComponent<VistaLightsLogger>();	
	}

	private IEnumerator Schedule() {
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
		double timerSpeed = timer.Speed;
		timer.speed = 0;
		int numberSteps = 2 + priorityQueue.GetCount();
		int stepsCompleted = 1;
		UpdateProgress(stepsCompleted, numberSteps);
		ShowSchedulingMask();
		yield return null;

		ClearAllSchedule();
		yield return null;

		for (int i = 0; i < priorityQueue.GetCount(); i++) {
			ShipScheduler shipScheduler = new ShipScheduler();
			ShipController ship = priorityQueue.GetShipWithPriority(i);
			shipScheduler.Ship = ship;
			shipScheduler.Schedule();

			stepsCompleted++;
			UpdateProgress(stepsCompleted, numberSteps);

			yield return null;
		}


		timer.Speed = timerSpeed;
		HideSchedulingMask();

		yield return null;
		yield return null;
		Scheduling = false;
	}

	private void ShowSchedulingMask() {
		schedulingMask.SetActive(true);
	}

	private void HideSchedulingMask() {
		schedulingMask.SetActive(false);
	}

	private void UpdateProgress(int currentStep, int totalStep) {
		progressBar.value = currentStep * 100 / totalStep;
	}

	private void ClearAllSchedule() { 
		ReservationManager reservationManager = GameObject.Find("MapUtil").GetComponent<ReservationManager>();
		reservationManager.ClearAll();

		foreach (ShipController ship in priorityQueue.Queue) {
			ship.schedule = null;
			ship.status = ShipStatus.Scheduling;
		}

		foreach (ShipController ship in waitList.Queue) {
			ship.schedule = null;
			ship.status = ShipStatus.RedSignal;
		}
	}

	public void EnqueueShip(ShipController ship) {
		priorityQueue.EnqueueShip(ship);
		RequestReschedule();
	}

	public void RemoveShip(ShipController ship) {
		priorityQueue.RemoveShip(ship);
	}

	public void ChangeShipPriority(ShipController ship, int priority) {
		priorityQueue.ChangePriority(ship, priority);
		// RequestReschedule();
		logger.LogChangeShipPriority(ship.Ship, priority);
	}

	public int GetShipPriority(ShipController ship) {
		return priorityQueue.GetPriority(ship);
	}

	void Update() {
		if (rescheduleRequested) {
			Scheduling = true;
			StartCoroutine(Schedule());
			rescheduleRequested = false;
		}
	}

	public void MoveShipToWaitList(ShipController ship) {
		priorityQueue.RemoveShip(ship);
		waitList.EnqueueShip(ship);
		// RequestReschedule();
	}

	public void MoveShipToPriorityQueue(ShipController ship) { 
		waitList.RemoveShip(ship);
		priorityQueue.EnqueueShip(ship);
		// RequestReschedule();
	}

	public int PriorityQueueLength() {
		return priorityQueue.GetCount();
	}

	public int ShipPositionInWaitList(ShipController ship) {
		return waitList.GetPriority(ship);
	}
	
}
