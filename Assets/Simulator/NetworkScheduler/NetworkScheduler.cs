using UnityEngine;
using System.Collections;
using System.Threading;

public class NetworkScheduler : MonoBehaviour {

	private bool rescheduleRequested = false;

	public PriorityQueue priorityQueue;
	public bool Scheduling = false;

	public void RequestReschedule() {
		rescheduleRequested = true;	
	}

	private void Schedule() {
		ReservationManager reservationManager = GameObject.Find("MapUtil").GetComponent<ReservationManager>();
		reservationManager.ClearAll();

		foreach (ShipController ship in priorityQueue.queue) {
			ship.schedule = null;
		}

		for (int i = 0; i < priorityQueue.GetCount(); i++) {
			ShipScheduler shipScheduler = new ShipScheduler();
			ShipController ship = priorityQueue.GetShipWithPriority(i);
			shipScheduler.Ship = ship;
			shipScheduler.Schedule();
		}
		Scheduling = false;
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
			Schedule();
		
			rescheduleRequested = false;
		}

		if (Scheduling) {
			Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
			timer.Pause();
		}
	}
	
}
