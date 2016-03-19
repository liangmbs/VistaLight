using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ShipListEntryController : MonoBehaviour {

	public ShipController shipController;

	public Button greenButton;
	public Button redButton;
	public Text priority;
	public Button priorityButton;
	public InputField priorityInput;
	public Text shipName;
	public Text type;
	public Text amount;
	public Text value;
	public Text dueTime;
	public Text eta;
	public Text status;

	public bool enterPriorityMode = false;
	public bool isGreenSignal = true;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		if (shipController == null) return;

		if (isGreenSignal) {
			greenButton.gameObject.SetActive(true);
			redButton.gameObject.SetActive(false);
		} else { 
			greenButton.gameObject.SetActive(false);
			redButton.gameObject.SetActive(true);
		}

		Ship ship = shipController.Ship;

		int priorityValue = shipController.GetShipPriority() + 1;
		priority.text = priorityValue.ToString();
		gameObject.GetComponent<RectTransform>().anchoredPosition =
			new Vector3(0, GetEntryYPos(), 0);

		status.text = shipController.status.ToString();

		shipName.text = ship.Name;

		type.text = ship.Industry.ToString();
		type.color = IndustryColor.GetIndustryColor(ship.Industry);

		amount.text = ship.cargo.ToString("N");
		value.text = ship.value.ToString("N");

		dueTime.text = ship.dueTime.ToString();

		DateTime unloadingEta = shipController.GetUnloadlingEta();
		if (unloadingEta == DateTime.MinValue) {
			eta.text = " - ";
		} else {
			eta.text = unloadingEta.ToString();
		}
	}

	public void EnterPriorityMode() {
		this.enterPriorityMode = true;

		priority.gameObject.SetActive(false);
		priorityButton.gameObject.SetActive(false);
		priorityInput.gameObject.SetActive(true);
		priorityInput.Select();
	}

	public void UpdatePriority() {
		this.enterPriorityMode = false;
		
		priority.gameObject.SetActive(true);
		priorityButton.gameObject.SetActive(true);
		priorityInput.gameObject.SetActive(false);

		int priorityValue = 0;
		NetworkScheduler networkScheduler = GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>();
		try {
			priorityValue = Int32.Parse(priorityInput.text);
        } catch (Exception) {
			return;
		}
		networkScheduler.ChangeShipPriority(shipController, priorityValue);
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
		isGreenSignal = !isGreenSignal;
		NetworkScheduler networkScheduler = GameObject.Find("NetworkScheduler").GetComponent<NetworkScheduler>();
		if (isGreenSignal) {
			networkScheduler.MoveShipToPriorityQueue(shipController);
			GameObject.Find ("BasicLoggerManager").GetComponent<VistaLightsLogger> ().LogRedGreenSignal (shipController.Ship, "green signal");
		} else { 
			networkScheduler.MoveShipToWaitList(shipController);
			GameObject.Find ("BasicLoggerManager").GetComponent<VistaLightsLogger> ().LogRedGreenSignal (shipController.Ship, "red signal");
		}
	}
}
