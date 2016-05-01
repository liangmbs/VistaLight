using SimpleJSON;
using UnityEngine;
using System.Collections;

// Example script for an object using the logger.
public class ExampleLogger : MonoBehaviour
{
	void Start()
	{
		TheLogger.instance.BeginRun("level", null);
	}
	
	void OnDestroy()
	{
		Debug.Log ("ExampleLogger");
		TheLogger.instance.EndRun(null);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			JSONClass details = new JSONClass();
			details ["key"] = "up";
			TheLogger.instance.TakeAction(1, details);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			JSONClass details = new JSONClass();
			details ["key"] = "down";
			TheLogger.instance.TakeAction(1, details);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			JSONClass details = new JSONClass();
			details ["key"] = "left";
			TheLogger.instance.TakeAction(1, details);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			JSONClass details = new JSONClass();
			details ["key"] = "right";
			TheLogger.instance.TakeAction(1, details);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			TheLogger.instance.TakeAction(2, null);
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			TheLogger.instance.TakeAction(3, null);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			TheLogger.instance.TakeAction(4, null);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			TheLogger.instance.TakeAction(5, null);
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			TheLogger.instance.EndRun(null);
			TheLogger.instance.BeginRun("level", null);
		}
	}
}
