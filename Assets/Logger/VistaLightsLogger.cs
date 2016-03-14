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
}

