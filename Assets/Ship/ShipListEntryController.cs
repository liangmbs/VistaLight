﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ShipListEntryController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public ShipController shipController;

	public ShipListController shipListController;

	public Button greenButton;
	public Button redButton;
	public Button SignalButton;
	public Button highlightButton;
	public Text priority;
	public InputField priorityInput;
	public Text shipName;
	public Text type;
	public Text amount;
	public Text value;
	public Text dueTime;
	public Text eta;
	public Text status;
	public Image background;

	public bool inDecisionMode = false;
	public bool isGreenSignal = true;

	public Vector3 Position {
		get {
			return gameObject.GetComponent<RectTransform> ().anchoredPosition;
		}
		set {
			gameObject.GetComponent<RectTransform> ().anchoredPosition = value;
		}
	}

	// Update is called once per frame
	void Update () {
		if (shipController == null) return;

		/*
		if (isGreenSignal) {
			greenButton.gameObject.SetActive(true);
			redButton.gameObject.SetActive(false);
		} else { 
			greenButton.gameObject.SetActive(false);
			redButton.gameObject.SetActive(true);
		}
		*/

		Ship ship = shipController.Ship;

		int priorityValue = shipController.GetShipPriority() + 1;

		if (!inDecisionMode) {
			priority.text = priorityValue.ToString ();
			Position = new Vector3 (0, GetEntryYPos (), 0);
		}

		status.text = shipController.status.ToString();

		shipName.text = ship.Name;

		type.text = ship.Industry.ToString();
		type.color = IndustryColor.GetIndustryColor(ship.Industry);

		amount.text = string.Format("{0:N0}", ship.cargo);
		value.text = string.Format("${0:N0}", ship.value * ship.cargo);

		dueTime.text = ship.dueTime.ToString(Map.DateTimeFormat);

		DateTime unloadingEta = shipController.GetUnloadlingEta();
		if (unloadingEta == DateTime.MinValue) {
			eta.text = " - ";
		} else {
			eta.text = unloadingEta.ToString(Map.DateTimeFormat);
		}
		if (unloadingEta > ship.dueTime) {
			eta.color = Color.red;
		} else {
			eta.color = Color.white;
		}
	}

	public void UpdatePriority() {
		
		priority.gameObject.SetActive(true);

		int priorityValue = 0;
		NetworkScheduler networkScheduler = GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>();
		try {
			priorityValue = Int32.Parse(priorityInput.text);
        } catch (Exception) {
			return;
		}
		networkScheduler.ChangeShipPriority(shipController, priorityValue);

		//shipListController.UpdateAllPriorityInput();
	}

	public void HighlightShip() {
		shipController.ToggleHighLight ();
	}

	private int GetEntryYPos() {
		NetworkScheduler networkScheduler = GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>();
		if (isGreenSignal) {
			int priorityValue = shipController.GetShipPriority();
			return -30 * priorityValue;
		} else {
			int priorityQueueSize = networkScheduler.PriorityQueueLength();
			int positionInWaitList = networkScheduler.ShipPositionInWaitList(shipController);
			return -30 * (priorityQueueSize + positionInWaitList) - 10;
		}
	}

	public void ToggleSignal() {
		if (inDecisionMode) {
			isGreenSignal = !isGreenSignal;
			NetworkScheduler networkScheduler = GameObject.Find ("NetworkScheduler").GetComponent<NetworkScheduler> ();
			if (isGreenSignal) {
				networkScheduler.MoveShipToPriorityQueue (shipController);
				GameObject.Find ("BasicLoggerManager").GetComponent<VistaLightsLogger> ().LogRedGreenSignal (shipController.Ship, "green signal");
			} else { 
				networkScheduler.MoveShipToWaitList (shipController);
				GameObject.Find ("BasicLoggerManager").GetComponent<VistaLightsLogger> ().LogRedGreenSignal (shipController.Ship, "red signal");
			}
			shipListController.UpdateAllPriorityInput();
		} else {
			NotificationSystem notificationSystem = GameObject.Find ("NotificationSystem").GetComponent<NotificationSystem> ();
			notificationSystem.Notify (NotificationType.Warning, "Signal to ship can only be set in decision phase");
		}
	}

	public void ShowNewPriority() {
		priorityInput.gameObject.SetActive (true);
		priorityInput.text = "";

		priority.gameObject.transform.Translate(new Vector3(40, 0, 0));
		status.gameObject.transform.Translate(new Vector3(40, 0, 0));
		shipName.gameObject.transform.Translate(new Vector3(40, 0, 0));
		type.gameObject.transform.Translate(new Vector3(40, 0, 0));
		amount.gameObject.transform.Translate(new Vector3(40, 0, 0));
		value.gameObject.transform.Translate(new Vector3(40, 0, 0));
		dueTime.gameObject.transform.Translate(new Vector3(40, 0, 0));
		eta.gameObject.transform.Translate(new Vector3(40, 0, 0));

		RectTransform rectTransform = gameObject.GetComponent<RectTransform> ();
		Vector2 sizeDelta = rectTransform.sizeDelta;
		rectTransform.sizeDelta = new Vector2 (sizeDelta.x + 40, sizeDelta.y);

		inDecisionMode = true;
	}

	public void HideNewPriority() {
		if (priorityInput.gameObject.activeSelf) {
			priorityInput.gameObject.SetActive (false);
			priority.gameObject.transform.Translate (new Vector3 (-40, 0, 0));
			status.gameObject.transform.Translate (new Vector3 (-40, 0, 0));
			shipName.gameObject.transform.Translate (new Vector3 (-40, 0, 0));
			type.gameObject.transform.Translate (new Vector3 (-40, 0, 0));
			amount.gameObject.transform.Translate (new Vector3 (-40, 0, 0));
			value.gameObject.transform.Translate (new Vector3 (-40, 0, 0));
			dueTime.gameObject.transform.Translate (new Vector3 (-40, 0, 0));
			eta.gameObject.transform.Translate (new Vector3 (-40, 0, 0));

			RectTransform rectTransform = gameObject.GetComponent<RectTransform> ();
			Vector2 sizeDelta = rectTransform.sizeDelta;
			rectTransform.sizeDelta = new Vector2 (sizeDelta.x - 40, sizeDelta.y);
		}

		inDecisionMode = false;
	}

	public void UpdatePriorityInput() {
		SetPriorityInput (shipController.GetShipPriority () + 1);
	}

	public void SetPriorityInput(int priority) {
		priorityInput.text = priority.ToString();
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		Vector3 pos = Position;
		pos.y += eventData.delta.y;
		Position = pos;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		PriorityQueue priorityQueue =
			GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>().priorityQueue;

		int newPriority = Math.Max(-1 * (int) Math.Round (Position.y / 30.0f), 0);
		newPriority = Math.Min(newPriority, priorityQueue.GetCount() - 1);
		int oldPriority = shipController.GetShipPriority ();

		// Set this ship entry's new location;
		Vector3 pos = Position;
		pos.y = newPriority * -30.0f;
		Position = pos;
		SetPriorityInput (newPriority + 1);

		// Swap with the other ship's
		ShipListEntryController otherEntry =
			shipListController.FindEntryWithPriority (newPriority);
		pos = otherEntry.Position;
		pos.y = oldPriority * -30.0f;
		otherEntry.Position = pos;
		otherEntry.SetPriorityInput (oldPriority + 1);
	}

	#endregion
}
