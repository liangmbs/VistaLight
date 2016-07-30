using UnityEngine;
using System.Collections;
using System;

public class ShipGenerationMapEventHandler : IMapEventHandler {

	private ShipGenerationEvent shipGenerationEvent;

	public ShipGenerationMapEventHandler(ShipGenerationEvent shipGenerationEvent) {
		this.shipGenerationEvent = shipGenerationEvent;
	}

	public void Process() {
		GameObject shipGO = PhotonNetwork.Instantiate("Ship", new Vector3(), new Quaternion(), 0);
		ShipVO shipVO = shipGO.GetComponent<ShipVO>();
		ShipController shipController = shipGO.GetComponent<ShipController>();

		Ship ship = shipGenerationEvent.Ship;
		ship.X = shipGenerationEvent.X;
		ship.Y = shipGenerationEvent.Y;

		shipVO.ship = ship;
		shipController.Ship = ship;
		shipController.shipGO = shipGO;
		shipController.shipVO = shipVO;
		shipController.ShipCreateTime = shipGenerationEvent.Time;

		GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>().EnqueueShip(shipController);

		GameObject.Find("ShipList").GetComponent<PhotonView>().RPC(
			"AddShip", PhotonTargets.All, shipController.GetComponent<PhotonView>().viewID);

		GameObject.Find ("BasicLoggerManager").GetComponent<VistaLightsLogger> ().LogShipGeneration(shipGenerationEvent);

		CreateNotification();
	}

	private void CreateNotification() {
		NotificationSystem notificationSystem = GameObject.Find("NotificationSystem").GetComponent<NotificationSystem>();

		Ship ship = shipGenerationEvent.Ship;
		string content = String.Format("{0} ship {1} arrived at anchor field and is waiting for scheduling.",
			ship.Industry.ToString(), ship.Name);

		notificationSystem.Notify (NotificationType.Information, content);
	}

}
