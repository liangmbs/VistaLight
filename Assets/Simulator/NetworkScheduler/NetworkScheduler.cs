using UnityEngine;
using System.Collections;

public class NetworkScheduler : MonoBehaviour {

	private bool rescheduleRequested = false;

	public PriorityQueue priorityQueue;

	public void RequestReschedule() {
		rescheduleRequested = true;	
	}

	private void Schedule() {
		for (int i = 0; i < priorityQueue.GetCount(); i++) {
			ShipScheduler shipScheduler = new ShipScheduler();
			ShipController ship = priorityQueue.GetShipWithPriority(i);
			shipScheduler.Ship = ship;
			shipScheduler.Schedule();
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
			Schedule();
			rescheduleRequested = false;
		}
	}
	
}
