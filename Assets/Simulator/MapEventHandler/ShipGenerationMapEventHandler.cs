using UnityEngine;
using System.Collections;
using System;

public class ShipGenerationMapEventHandler : MonoBehaviour, IMapEventHandler {

	private ShipGenerationEvent shipGenerationEvent;

	public ShipGenerationMapEventHandler(ShipGenerationEvent shipGenerationEvent) {
		this.shipGenerationEvent = shipGenerationEvent;
	}

	public void Process() {
		GameObject shipPrefab = Resources.Load("Ship") as GameObject;

		
		GameObject shipGO = GameObject.Instantiate(shipPrefab);
		ShipVO shipVO = shipGO.GetComponent<ShipVO>();
		ShipController shipController = shipGO.GetComponent<ShipController>();

		Ship ship = shipGenerationEvent.Ship;
		ship.X = shipGenerationEvent.X;
		ship.Y = shipGenerationEvent.Y;
		Debug.Log(String.Format("Ship created at {0}, {1}", ship.X, ship.Y));
		shipVO.ship = ship;
		shipController.ship = ship;
		shipController.shipGO = shipGO;
		shipController.shipVO = shipVO;

		GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>().EnqueueShip(shipController);
	}

}
