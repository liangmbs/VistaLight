using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ShipGeneratorEntryController : MonoBehaviour {

	public Ship ship;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Ship Ship { 
		get { return ship; }
		set { 
			ship = value;
		}
	}

	public void UpdateName() {
		ship.Name = gameObject.transform.Find("Name").gameObject.GetComponent<InputField>().text;
		this.UpdateInformDisplay();
	}

	public void UpdateCargoType() {
		ship.Industry = (IndustryType)gameObject.transform.Find("CargoType").gameObject.GetComponent<Dropdown>().value;
		this.UpdateInformDisplay();
    }

	public void UpdateTonnage() {
		try {
			ship.cargo = int.Parse(gameObject.transform.Find("Tonnage").gameObject.GetComponent<InputField>().text);
		} catch (Exception) {
			Debug.Log("Invalid value for tonnage field.");
		}
		this.UpdateInformDisplay();
	}

	public void UpdateValue() {
		try {
			ship.value = int.Parse(gameObject.transform.Find("Value").gameObject.GetComponent<InputField>().text);
		} catch (Exception) {
			Debug.Log("Invalid value for value field.");
		}
		this.UpdateInformDisplay();
	}

	public void UpdateDueTime() {
		try {
			ship.dueTime = DateTime.Parse(gameObject.transform.Find("DueTime").gameObject.GetComponent<InputField>().text);
		} catch (Exception) {
			Debug.Log("Invalid value for due time field.");
		}
		this.UpdateInformDisplay();
	}

	public void UpdateInformDisplay() {
		gameObject.transform.Find("Id").gameObject.GetComponent<Text>().text = ship.shipID.ToString();
		gameObject.transform.Find("Name").gameObject.GetComponent<InputField>().text = ship.Name;
		gameObject.transform.Find("CargoType").gameObject.GetComponent<Dropdown>().value = (int)ship.Industry;
		gameObject.transform.Find("CargoType").gameObject.GetComponent<Dropdown>().captionText.text = ship.Industry.ToString();
        gameObject.transform.Find("Tonnage").gameObject.GetComponent<InputField>().text = ship.cargo.ToString();
		gameObject.transform.Find("Value").gameObject.GetComponent<InputField>().text = ship.value.ToString();
		gameObject.transform.Find("DueTime").gameObject.GetComponent<InputField>().text = ship.dueTime.ToString(Map.DateTimeFormat);
	}

	public void UpdateDock(List<Dock> docks) {
		throw new NotImplementedException();
	}
}
