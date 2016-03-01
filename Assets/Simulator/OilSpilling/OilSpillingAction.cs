using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public enum OilSpillSolution { 
	None,
	Burn,
	Dispersant,
	Skimmers
}

public class OilSpillingAction : MonoBehaviour {

	public OilSpillingController OilSpillingController;
	public MapController MapController;
	public WelfareCounter WelfareCounter;

	public OilSpillSolution solution = OilSpillSolution.None;

	private List<Connection> disconnectedMapConnections = new List<Connection>();

	public Toggle BuringToggle;
	public Toggle DispersantToggle;
	public Toggle SkimmersToggle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (solution == OilSpillSolution.None) return;
		
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();

		double speed = 0;
		double welfareImpact = 0;

		switch (solution) {
		case OilSpillSolution.Burn:
			speed = 0.1;
			welfareImpact = 1.5 / 10000;
			break;
		case OilSpillSolution.Dispersant:
			speed = 0.01;
			welfareImpact = 1 / 10000;
			break;
		case OilSpillSolution.Skimmers:
			speed = 0.02;
			break;
		}

		double oilCleaned = timer.TimeElapsed.TotalSeconds * speed;
		OilSpillingController.Amount -= oilCleaned; 
		if (OilSpillingController.Amount <= 0) {
			OilSpillingController.Amount = 0;
			if (solution == OilSpillSolution.Burn || solution == OilSpillSolution.Skimmers) {
				RestartTraffic();
			}
			solution = OilSpillSolution.None;
		}

		double welfareChange = welfareImpact * oilCleaned;
		WelfareCounter.ReduceWelfare(welfareChange);
			
	}

	public void EnableAllToggles() {
		BuringToggle.interactable = true;
		DispersantToggle.interactable = true;
		SkimmersToggle.interactable = true;
	}

	public void DisableAllToggles() {
		BuringToggle.interactable = false;
		DispersantToggle.interactable = false;
		SkimmersToggle.interactable = false;
	}

	private bool isInOilSpill(Node node) {
		double distance = Math.Pow(Math.Pow(node.X - OilSpillingController.position.x, 2) + Math.Pow(node.Y - OilSpillingController.position.y, 2), 0.5);
		return distance <= OilSpillingController.Radius;
	}

	private void StopTraffic() {
		Map map = MapController.Map;
		foreach (Connection connection in map.connections) {
			if (isInOilSpill(connection.StartNode) || isInOilSpill(connection.EndNode)) {
				disconnectedMapConnections.Add(connection);
				connection.StartNode.RemoveConnection(connection);
				connection.EndNode.RemoveConnection(connection);
			} 
		}

		foreach (Connection connection in disconnectedMapConnections) {
			MapController.RemoveConnection(MapController.GetConnectionGO(connection));
		}

		GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>().RequestReschedule();
	}

	private void RestartTraffic() {
		Map map = MapController.Map;
		foreach (Connection connection in disconnectedMapConnections) {
			MapController.CreateConnectionGameObject(connection);
			connection.StartNode.AddConnection(connection);
			connection.EndNode.AddConnection(connection);
			map.AddConnection(connection);
		}
		GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>().RequestReschedule();
	}

	public void Burn() {
		StopTraffic();
		solution = OilSpillSolution.Burn;
		DisableAllToggles();
	}

	public void Dispersant() {
		solution = OilSpillSolution.Dispersant;
		DisableAllToggles();
	}

	public void Skimmers() {
		StopTraffic();
		DisableAllToggles();
		solution = OilSpillSolution.Skimmers;
	}
}
