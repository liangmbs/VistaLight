using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShipPanelController : MonoBehaviour, IShipListController {

	public GameObject shipGenreateEntryPrefab;
	public GameObject shipListPanel;
	public Map map = null;
	private int nextShipId = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddShip() {
		if (map == null) {
			return;
		}

		// Create new ship
		Ship ship = new Ship();
		ship.shipID = nextShipId;
		nextShipId++;

		// Create Entry
		GameObject entry = CreateShipEntry(ship);
		

		// Add ship to map
		map.AddShip(ship);
	}

	public GameObject CreateShipEntry(Ship ship) {
		// Entry Height
		RectTransform entryTransform = shipGenreateEntryPrefab.GetComponent<RectTransform>();
		float entryHeight = entryTransform.sizeDelta.y;

		// Enlarge ship list panel
		RectTransform transform = shipListPanel.GetComponent<RectTransform>();
		Vector3 positionToPlaceNewEntry = new Vector3(0, -transform.sizeDelta.y, 0);
		transform.sizeDelta = new Vector2(transform.sizeDelta.x, transform.sizeDelta.y + entryHeight);

		// Add a new ship entry
		GameObject entry = GameObject.Instantiate(shipGenreateEntryPrefab);
		entry.transform.SetParent(shipListPanel.transform);
		entryTransform = entry.GetComponent<RectTransform>();
		entryTransform.localPosition = positionToPlaceNewEntry;

		// Link ship with the entry
		ShipGeneratorEntryController controller = entry.GetComponent<ShipGeneratorEntryController>();
		controller.Ship = ship;
		controller.UpdateInformDisplay();


		return entry;
	}

	public void RegenerateShip(List<Ship> ships) {
		foreach(Ship ship in ships) {
			CreateShipEntry(ship);
		}
	}

	public void ClearShips() {
		for (int i = 0; i < shipListPanel.transform.childCount; i++) {
			GameObject entry = shipListPanel.transform.GetChild(i).gameObject;
			ShipGeneratorEntryController entryController = entry.GetComponent<ShipGeneratorEntryController>();
			if (entryController) {
				Destroy(entry);
			}
		}
	}

	public void UpdateDockInformation(List<Dock> docks) {
		for (int i = 0; i < shipListPanel.transform.childCount; i++) {
			GameObject entry = shipListPanel.transform.GetChild(i).gameObject;
			ShipGeneratorEntryController entryController = entry.GetComponent<ShipGeneratorEntryController>();
			if (entryController) {
				entryController.UpdateDock(docks);
			}
		}
	}

	public void AddShip(Ship ship) {
		throw new NotImplementedException();
	}

	public void ClearList() {
		throw new NotImplementedException();
	}
}
