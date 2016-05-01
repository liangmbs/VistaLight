using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ShipGenerationEventSidePanelController : MonoBehaviour {

    public ShipGenerationEvent shipGenerationEvent;
    public InputField timeInput;
	public InputField shipIdInput;
	public MapController mapController;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateDisplay() {
        if (shipGenerationEvent == null) return;

        timeInput.text = shipGenerationEvent.Time.ToString(Map.DateTimeFormat);

		if (shipGenerationEvent.Ship != null) {
			shipIdInput.text = shipGenerationEvent.Ship.shipID.ToString();
		} else {
			shipIdInput.text = "None";
		}
    }

    public void UpdateData() {
        shipGenerationEvent.Time = DateTime.Parse(timeInput.text);
		int shipId = Int32.Parse(shipIdInput.text);
		Ship ship = null;
		try {
			ship = mapController.GetShipById(shipId);
			shipIdInput.text = ship.shipID.ToString();
			shipGenerationEvent.Ship = ship;
		} catch (KeyNotFoundException) {
			Debug.LogError(String.Format("Ship {0} is not created!", shipId));
		}
		

        UpdateDisplay();
    }
}
