using UnityEngine;
using System.Collections;

public class ShipScheduler {

	private Ship ship;
	public Ship Ship { 
		set { ship = value; }
	}

	public void Schedule() {
		Debug.Log(string.Format("Scheduling ship {0}", ship.Name));
		Debug.Log("Find all paths");
		Debug.Log("Path to tasks");
		Debug.Log("Find path with best ETA");
		Debug.Log("Reserver that path");
	}

	private void FindAllPaths() {
		if (ship.cargo == 0) {
			FindAllPathsToLeaveMap();
        } else {
			FindAllPathsToUnloadAndLeaveMap();
		}
	}

	private void FindAllPathsToLeaveMap() { 
	}

	private void FindAllPathsToUnloadAndLeaveMap() { 
	}

}
