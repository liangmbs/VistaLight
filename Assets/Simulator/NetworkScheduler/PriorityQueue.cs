using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PriorityQueue : MonoBehaviour {

	public List<ShipController> queue = new List<ShipController>();

	public void EnqueueShip(ShipController ship) {
		queue.Add(ship);
	}

	public void RemoveShip(ShipController ship) {
		queue.Remove(ship);
	}

	public void ChangePriority(ShipController ship, int priority) {
		queue.Remove(ship);
		if (priority >= queue.Count - 1) {
			queue.Add (ship);
		} else {
			queue.Insert (priority - 1, ship);
		}
	}

	public int GetPriority(ShipController ship) {
		return queue.FindIndex(item => item == ship);
	}

	public int GetCount() {
		return queue.Count;	
	}

	public ShipController GetShipWithPriority(int priority) {
		return queue[priority];
	}

	public void Clear() {
		queue.Clear ();
	}
	
}
