﻿using System;
using UnityEngine;

class MapEventTool : IMapEditorTool {

	private MapController mapController;
	private GameObject selectedMapEvent = null;

	public enum MapEventType {
		ShipGeneration, OilSpilling
	}

	private void SelectMapEvent(GameObject mapEvent) {
		if(selectedMapEvent != null) {
			selectedMapEvent.GetComponent<MapEventVO>().IsSelected = false;
		}
		selectedMapEvent = mapEvent;
		mapEvent.GetComponent<MapEventVO>().IsSelected = true;
	}

	private void DeselectMapEvent() {
		if (selectedMapEvent != null) {
			selectedMapEvent.GetComponent<MapEventVO>().IsSelected = false;
			selectedMapEvent = null;
		}
	}

	public MapEventTool(MapController mapController) {
		this.mapController = mapController;
	}

	public void Destory() {
		DeselectMapEvent();
	}

	public void RespondMouseLeftClick() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.collider != null && ray.collider.tag == "Event") {
			SelectMapEvent(ray.collider.gameObject);
		} else if (ray.collider.tag == "Background") {
			GameObject mapEvent = mapController.AddMapEvent(ray.point);
			SelectMapEvent(mapEvent);
		}
	}

	public void RespondMouseLeftUp() {
	}

	public void RespondMouseMove(float x, float y) {
	}

	public void RespondMouseRightClick() {
		DeselectMapEvent();
	}
}
