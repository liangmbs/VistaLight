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
		
		GameObject oilSpillingGO = GameObject.Instantiate(oilSpillingPrefab);

		OilSpillingController oilSpillingController = oilSpillingGO.GetComponent<OilSpillingController>();
		oilSpillingController.position = new Vector2((float)OilSpillingEvent.X, (float)OilSpillingEvent.Y);
		oilSpillingController.Radius = OilSpillingEvent.Radius;
		oilSpillingController.Amount = OilSpillingEvent.Amount;

		OilSpillingAction action = GameObject.Find("OilSpillingAction").GetComponent<OilSpillingAction>();
		action.OilSpillingController = oilSpillingController;
		action.EnableAllToggles();

		ShowAlarm();

		ShowNotification();

		GameObject.Find ("BasicLoggerManager").GetComponent<VistaLightsLogger> ().LogOilSpilling (OilSpillingEvent);

		PauseGame();
	}

	private void ShowAlarm() {
		GameObject.Find("Alarm").GetComponent<Image>().enabled = true;
	}

	private void ShowNotification() {
		NotificationSystem notificationSystem = GameObject.Find("NotificationSystem").GetComponent<NotificationSystem>();

		Notification notification = new Notification();
		notification.content = "Oil spilling happens";

		notificationSystem.AddNotification(notification);
	}

	private void PauseGame() {
		TimeWidgetController timeWidgetController = 
			GameObject.Find ("TimeWidget").GetComponent<TimeWidgetController> ();
		timeWidgetController.PauseGame ();
	}

}
