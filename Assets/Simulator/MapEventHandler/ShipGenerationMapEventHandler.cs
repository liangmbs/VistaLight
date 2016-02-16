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

		Ship ship = shipGenerationEvent.Ship;
		ship.X = shipGenerationEvent.X;
		ship.Y = shipGenerationEvent.Y;
		shipVO.ship = ship;
	}

}
