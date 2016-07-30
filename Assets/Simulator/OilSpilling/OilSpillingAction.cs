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
	public BudgetCounter budgetCounter;

	public OilSpillSolution solution = OilSpillSolution.None;

	private List<Connection> disconnectedMapConnections = new List<Connection>();

	public Toggle BuringToggle;
	public Toggle DispersantToggle;
	public Toggle SkimmersToggle;

	public VistaLightsLogger logger;

	public double speed;
	public double welfareImpact;

	public PriorityQueue priorityQueue;
	public PriorityQueue waitList;

	public NotificationSystem notificationSystem;

	// Use this for initialization
	void Start () {
	
	}

	void Awake() {
		logger = GameObject.Find("BasicLoggerManager").GetComponent<VistaLightsLogger>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (solution == OilSpillSolution.None) return;
		
		Timer timer = GameObject.Find("Timer").GetComponent<Timer>();

		double oilCleaned = timer.TimeElapsed.TotalSeconds * speed;
		OilSpillingController.Amount -= oilCleaned; 
		if (OilSpillingController.Amount <= 0) {
			OilSpillingController.Amount = 0;
			if (solution == OilSpillSolution.Burn || solution == OilSpillSolution.Skimmers) {
				RestartTraffic();
			}
			solution = OilSpillSolution.None;
			RecoverTrafficSpeed ();

			string content = string.Format ("Oil has been cleaned up");
			GameObject.Find ("NotificationSystem").GetComponent<NotificationSystem> ().Notify (NotificationType.Success, content);
		}

		double welfareChange = welfareImpact * oilCleaned;
		WelfareCounter.ReduceWelfare(welfareChange);
			
	}

	private void SetCleaningSpeed ()
	{
		switch (solution) {
		case OilSpillSolution.Burn:
			speed = 1.0 * OilSpillingController.Amount / 10 / 3600;
			welfareImpact = 1.5 / 10000;
			break;
		case OilSpillSolution.Dispersant:
			speed = 1.0 * OilSpillingController.Amount / 48 / 3600;
			welfareImpact = 1 / 10000;
			break;
		case OilSpillSolution.Skimmers:
			speed = 1.0 * OilSpillingController.Amount / 24 / 3600;
			break;
		}
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

	public void SlowDownTraffic(double speed) {
		Map map = MapController.Map;
		foreach (Connection connection in map.connections) {
			if (isInOilSpill(connection.StartNode) || isInOilSpill(connection.EndNode)) {
				connection.Speed = speed;
			} 
		}
		GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>().RequestReschedule();
	}

	public void RecoverTrafficSpeed() {
		Map map = MapController.Map;
		foreach (Connection connection in map.connections) {
			if (isInOilSpill(connection.StartNode) || isInOilSpill(connection.EndNode)) {
				double shipSpeed = GameObject.Find ("SceneSetting").GetComponent<SceneSetting> ().ShipSpeed;
				connection.Speed = shipSpeed;
			} 
		}
		GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>().RequestReschedule();
	}

	public bool hasShipInOilArea() {
		foreach(ShipController ship in priorityQueue.Queue) {
			double distance = Math.Pow(Math.Pow(ship.Ship.X - OilSpillingController.position.x, 2) + Math.Pow(ship.Ship.Y - OilSpillingController.position.y, 2), 0.5);
			if (distance < OilSpillingController.Radius) {
				return true;
			}
		}

		foreach(ShipController ship in waitList.Queue) {
			double distance = Math.Pow(Math.Pow(ship.Ship.X - OilSpillingController.position.x, 2) + Math.Pow(ship.Ship.Y - OilSpillingController.position.y, 2), 0.5);
			if (distance < OilSpillingController.Radius) {
				return true;
			}
		}

		return false;
	}

	public void Burn() {

		if (hasShipInOilArea()) {
			notificationSystem.Notify (NotificationType.Warning, 
				"Cannot burn. There are one or more ships in the oil-polluted area");
			return;
		}

		StopTraffic();
		solution = OilSpillSolution.Burn;

		budgetCounter.SpendMoney (1000000);

		SetCleaningSpeed ();

		DisableAllToggles();

		logger.LogOilCleaning (OilSpillSolution.Burn);
	}

	public void Dispersant() {
		solution = OilSpillSolution.Dispersant;

		budgetCounter.SpendMoney (2000000);

		SetCleaningSpeed ();

		double shipSpeedInDispersant = GameObject.Find ("SceneSetting").GetComponent<SceneSetting> ().ShipSpeedInDispersantArea;
		SlowDownTraffic (shipSpeedInDispersant);

		DisableAllToggles();

		logger.LogOilCleaning (OilSpillSolution.Dispersant);
	}

	public void Skimmers() {

		if (hasShipInOilArea()) {
			notificationSystem.Notify (NotificationType.Warning, 
				"Cannot use skimmers. There are one or more ships in the oil-polluted area");
			return;
		}

		StopTraffic();

		solution = OilSpillSolution.Skimmers;
		SetCleaningSpeed ();
		budgetCounter.SpendMoney (3000000);

		DisableAllToggles();

		logger.LogOilCleaning (OilSpillSolution.Skimmers);
	}
}
