using System;
using UnityEngine;
using System.Collections;

public class DockUtilizationCounter : MonoBehaviour {

	public MapController mapController;
	public Timer timer;

	public double averageUtilization = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public double CalculateAverageUtilization() {
		TimeSpan totalUtilizedTime = TimeSpan.Zero;
		int numberOfDocks = 0;
		foreach (Dock dock in mapController.Map.docks) {
			totalUtilizedTime += dock.utilizedTime;
			numberOfDocks++;
		}

		TimeSpan totalTime = timer.VirtualTime - timer.gameStartTime;

		averageUtilization = totalUtilizedTime.TotalSeconds / numberOfDocks / totalTime.TotalSeconds;
		return averageUtilization;
	}
		
}
