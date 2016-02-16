using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PriorityQueue : MonoBehaviour {

	private List<Ship> queue = new List<Ship>();

	public void EnqueueShip(Ship ship) {
		queue.Add(ship);
	}

	public void RemoveShip(Ship ship) {
		queue.Remove(ship);
	}

	public void ChangePriority(Ship ship, int priority) {
		queue.Remove(ship);
		queue.Insert(priority, ship);
	}

	public int GetPriority(Ship ship) {
		return queue.FindIndex(item => item == ship);
	}

	public int GetCount() {
		return queue.Count;	
	}

	public Ship GetShipWithPriority(int priority) {
		return queue[priority];
	}
	
}
