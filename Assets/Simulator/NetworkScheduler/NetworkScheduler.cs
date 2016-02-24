using UnityEngine;
using System.Collections;
using System.Threading;

public class NetworkScheduler : MonoBehaviour {

	private bool rescheduleRequested = false;

	public PriorityQueue priorityQueue;
	public PriorityQueue waitList;
	public bool Scheduling = false;

	public void RequestReschedule() {
		rescheduleRequested = true;	
	}

	private IEnumerator Schedule() {
		ClearAllSchedule();

		for (int i = 0; i < priorityQueue.GetCount(); i++) {
			ShipScheduler shipScheduler = new ShipScheduler();
			ShipController ship = priorityQueue.GetShipWithPriority(i);
			shipScheduler.Ship = ship;
			shipScheduler.Schedule();
			yield return null;
		}
		Scheduling = false;
	}

	private void ClearAllSchedule() { 
		ReservationManager reservationManager = GameObject.Find("MapUtil").GetComponent<ReservationManager>();
		reservationManager.ClearAll();

		foreach (ShipController ship in priorityQueue.queue) {
			ship.schedule = null;
		}

		foreach (ShipController ship in waitList.queue) {
			ship.schedule = null;
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
		RequestReschedule();
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

		/*
		if (Scheduling) {
			Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
			timer.Pause();
		}
		*/
	}

	public void MoveShipToWaitList(ShipController ship) {
		priorityQueue.RemoveShip(ship);
		waitList.EnqueueShip(ship);
		RequestReschedule();
	}

	public void MoveShipToPriorityQueue(ShipController ship) { 
		waitList.RemoveShip(ship);
		priorityQueue.EnqueueShip(ship);
		RequestReschedule();
	}

	public int PriorityQueueLength() {
		return priorityQueue.queue.Count;
	}

	public int ShipPositionInWaitList(ShipController ship) {
		return waitList.GetPriority(ship);
	}
	
}
