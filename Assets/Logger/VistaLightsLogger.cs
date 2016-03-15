using UnityEngine;
using System.Collections;
using SimpleJSON;
using System;

public class VistaLightsLogger : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	void OnDestroy() {

	}

	// Update is called once per frame
	void Update () {
		LogKeyStroke();
    }

	public void LogTimer(double speed) {
		JSONClass details = new JSONClass();
		details["timer"] = speed.ToString();
		TheLogger.instance.TakeAction(1, details);
	}

	public void LogKeyStroke() {
		foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
			if (Input.GetKeyDown(kcode)) {
				JSONClass details = new JSONClass();
				details["keystroke"] = kcode.ToString();
				details["mouse_x"] = Input.mousePosition.x.ToString();
				details["mouse_y"] = Input.mousePosition.y.ToString();
				TheLogger.instance.TakeAction(1, details);
			}
		}
	}

	public void LogMouseMove() {
		float mouse_move_x = Input.GetAxis("Mouse X");
		float mouse_move_y = Input.GetAxis("Mouse Y");
		if (mouse_move_x != 0 || mouse_move_y != 0) {
			JSONClass details = new JSONClass();
			details["mouse_x"] = Input.mousePosition.x.ToString();
			details["mouse_y"] = Input.mousePosition.y.ToString();
			TheLogger.instance.TakeAction(1, details);
		}
	}

	public void LogChangeShipPriority(Ship ship, int new_priority) {
		JSONClass details = new JSONClass();
		details["ship_id"] = ship.shipID.ToString();
		details["new_priority"] = new_priority.ToString();
		TheLogger.instance.TakeAction(1, details);
	}

	public void LogGameOver(double money, double welfare) {
		JSONClass details = new JSONClass();
		details["budget"] = money.ToString();
		details["welfare"] = welfare.ToString();
		TheLogger.instance.TakeAction(1, details);
	}

	public void LogOilCleaning(OilSpillSolution solution) {
		JSONClass details = new JSONClass();
		details ["solution"] = solution.ToString ();
		TheLogger.instance.TakeAction(1, details);
	}

	public void LogOilSpilling(OilSpillingEvent oilSpillingEvent) {
		JSONClass details = new JSONClass();

		details ["map_event"] = "Oil Spilling";
		details ["x"] = oilSpillingEvent.X.ToString ();
		details ["y"] = oilSpillingEvent.Y.ToString ();
		details ["radius"] = oilSpillingEvent.Radius.ToString();
		details ["amount"] = oilSpillingEvent.Amount.ToString();

		TheLogger.instance.TakeAction(1, details);
	}

	public void LogShipGeneration(ShipGenerationEvent shipGenerationEvent) {
		JSONClass details = new JSONClass();
		details ["map_event"] = "Ship Generation";
		details ["x"] = shipGenerationEvent.X.ToString ();
		details ["y"] = shipGenerationEvent.Y.ToString ();

		details ["ship_id"] = shipGenerationEvent.Ship.shipID.ToString ();
		details ["name"] = shipGenerationEvent.Ship.Name;
		details ["industry"] = shipGenerationEvent.Ship.Industry.ToString();
		details ["cargo"] = shipGenerationEvent.Ship.cargo.ToString();
		details ["value"] = shipGenerationEvent.Ship.value.ToString();
		details ["due_time"] = shipGenerationEvent.Ship.dueTime.ToString ();

		TheLogger.instance.TakeAction(1, details);
	}
}

