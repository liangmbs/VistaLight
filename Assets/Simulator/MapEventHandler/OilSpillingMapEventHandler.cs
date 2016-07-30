using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class OilSpillingMapEventHandler : IMapEventHandler {

	public OilSpillingEvent OilSpillingEvent;

	public OilSpillingMapEventHandler(OilSpillingEvent oilSpillingEvent) {
		this.OilSpillingEvent = oilSpillingEvent;
	}

	public void Process() {
		GameObject oilSpillingPrefab = Resources.Load("OilSpilling") as GameObject;
		
		GameObject oilSpillingGO = PhotonNetwork.Instantiate("oilSpillingPrefab", new Vector3(), new Quaternion(), 0);

		OilSpillingController oilSpillingController = oilSpillingGO.GetComponent<OilSpillingController>();
		oilSpillingController.position = new Vector2((float)OilSpillingEvent.X, (float)OilSpillingEvent.Y);
		oilSpillingController.Radius = OilSpillingEvent.Radius;
		oilSpillingController.Amount = OilSpillingEvent.Amount;

		OilSpillingAction action = GameObject.Find("OilSpillingAction").GetComponent<OilSpillingAction>();
		action.OilSpillingController = oilSpillingController;
		action.EnableAllToggles();

		double shipSpeedInOilSpilling = GameObject.Find ("SceneSetting").GetComponent<SceneSetting> ().ShipSpeedInOilSpill;
		action.SlowDownTraffic (shipSpeedInOilSpilling);

		ShowNotification();
		OpenOilCleaningTab ();

		GameObject.Find ("BasicLoggerManager").GetComponent<VistaLightsLogger> ().LogOilSpilling (OilSpillingEvent);

		PauseGame();
	}

	private void ShowNotification() {
		NotificationSystem notificationSystem = GameObject.Find("NotificationSystem").GetComponent<NotificationSystem>();

		string content = "Oil spilling happens. Please take an oil-cleaning action in the Oil Cleaning tab";

		notificationSystem.Notify (NotificationType.Disaster, content);
	}

	private void PauseGame() {
		RoundManager roundManager = GameObject.Find ("RoundManager").GetComponent<RoundManager> ();
		roundManager.StartDecisionPhase ();
	}

	private void OpenOilCleaningTab() {
		GameObject.Find ("LayoutManager").GetComponent<WindowManager> ().ShowTab2 ();
	}

}
