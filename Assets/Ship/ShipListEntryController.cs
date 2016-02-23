using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ShipListEntryController : MonoBehaviour {

	public ShipController shipController;

	public Button greenButton;
	public Button redButton;
	public Text id;
	public Text priority;
	public Button priorityButton;
	public InputField priorityInput;
	public Text shipName;
	public Text type;
	public Text amount;
	public Text value;
	public Text dueTime;
	public Text eta;

	public bool enterPriorityMode = false;

	// Use this for initialization
	void Start () {
		greenButton = transform.FindChild("GreenLight").gameObject.GetComponent<Button>();	
		redButton = transform.FindChild("RedLight").gameObject.GetComponent<Button>();	
		id = transform.FindChild("Id").gameObject.GetComponent<Text>();	
		priority = transform.FindChild("PriorityLabel").gameObject.GetComponent<Text>();	
		priorityButton = transform.FindChild("PriorityButton").gameObject.GetComponent<Button>();	
		priorityInput = transform.FindChild("Priority").gameObject.GetComponent<InputField>();	
		shipName = transform.FindChild("Name").gameObject.GetComponent<Text>();	
		type = transform.FindChild("Type").gameObject.GetComponent<Text>();	
		amount = transform.FindChild("Amount").gameObject.GetComponent<Text>();	
		value = transform.FindChild("UnitValue").gameObject.GetComponent<Text>();	
		dueTime = transform.FindChild("DueTime").gameObject.GetComponent<Text>();	
		eta = transform.FindChild("ETA").gameObject.GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (shipController == null) return;

		Ship ship = shipController.ship;
		id.text = ship.shipID.ToString();

		int priorityValue = shipController.GetShipPriority() + 1;
		priority.text = priorityValue.ToString();
		gameObject.GetComponent<RectTransform>().anchoredPosition =
			new Vector3(0, -priorityValue * 30, 0);

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

		Debug.Log("Focus");
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
		shipController.highLighted = !shipController.highLighted;
		if (shipController.highLighted) {
			shipName.fontStyle = FontStyle.Bold;
		} else {
			shipName.fontStyle = FontStyle.Normal;
		}
	}
}
