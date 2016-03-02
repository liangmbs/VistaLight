using UnityEngine;
using System.Collections;
using System;

public class ShipGenerationMapEventHandler : IMapEventHandler {

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

		shipVO.ship = ship;
		shipController.ship = ship;
		shipController.shipGO = shipGO;
		shipController.shipVO = shipVO;

		GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>().EnqueueShip(shipController);
		GameObject.Find("ShipList").GetComponent<ShipListController>().AddShip(shipController);
	}

}
