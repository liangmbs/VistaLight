using UnityEngine;
using System.Collections;

public class NetworkScheduler : MonoBehaviour {

	private bool rescheduleRequested = false;
	private ShipScheduler shipScheduler = new ShipScheduler();

	public PriorityQueue priorityQueue;

	public void RequestReschedule() {
		rescheduleRequested = true;	
	}

	private void Schedule() {
		for (int i = 0; i < priorityQueue.GetCount(); i++) {
			Ship ship = priorityQueue.GetShipWithPriority(i);
			shipScheduler.Ship = ship;
			shipScheduler.Schedule();
		} 
	}

	public void EnqueueShip(Ship ship) {
		priorityQueue.EnqueueShip(ship);
		RequestReschedule();
	}

	public void RemoveShip(Ship ship) {
		priorityQueue.RemoveShip(ship);
	}

	public void ChangeShipPriority(Ship ship, int priority) {
		priorityQueue.ChangePriority(ship, priority);
		RequestReschedule();
	}

	public int GetShipPriority(Ship ship) {
		return priorityQueue.GetPriority(ship);
	}

	void Update() {
		if (rescheduleRequested) {
			Schedule();
			rescheduleRequested = false;
		}
	}
	
}
