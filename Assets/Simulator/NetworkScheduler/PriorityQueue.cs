using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PriorityQueue : MonoBehaviour {

	public IEnumerable<ShipController> Queue {
		get {
			foreach (int ID in queue) {
				yield return IDtoShipController (ID);
			}
		}
	}

	private List<int> queue = new List<int>();

	public List<ShipController> ROQueue = new List<ShipController>();

	void Start() {
		GetComponent<PhotonView> ().ObservedComponents.Add (this);
	}

	void Update() {
		ROQueue.Clear ();
		ROQueue.AddRange (Queue);
	}

	public void EnqueueShip(ShipController ship) {
		queue.Add(ship.GetComponent<PhotonView>().viewID);
	}

	public void RemoveShip(ShipController ship) {
		queue.Remove(ship.GetComponent<PhotonView>().viewID);
	}

	public void ChangePriority(ShipController ship, int priority) {
		RemoveShip(ship);
		if (priority >= queue.Count + 1) {
			EnqueueShip (ship);
		} else {
			queue.Insert (priority - 1, ship.GetComponent<PhotonView>().viewID);
		}
	}

	public int GetPriority(ShipController ship) {
		return queue.IndexOf(ship.GetComponent<PhotonView>().viewID) + 1;
	}

	public int GetCount() {
		return queue.Count;	
	}

	public ShipController GetShipWithPriority(int priority) {
		return IDtoShipController(queue[priority]);
	}

	public void SwapPriority(int priority, int otherPriority) {
		int temp = queue [priority - 1];
		queue [priority - 1] = queue [otherPriority - 1];
		queue [otherPriority - 1] = temp;
	}

	public void Clear() {
		queue.Clear ();
	}

	private static ShipController IDtoShipController(int ID) {
		return PhotonView.Find (ID).gameObject.GetComponent<ShipController> ();
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (queue.Count);
			foreach (int ID in queue) {
				stream.SendNext (ID);
			}
		} else {
			queue.Clear ();
			int count = (int)stream.ReceiveNext ();
			for (int i = 0; i < count; i++) {
				queue.Add((int)stream.ReceiveNext());
			}
		}
	}
}
