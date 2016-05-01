using System;
using UnityEngine;

public enum MapEventType {
	ShipGeneration, OilSpilling
}

class MapEventTool : IMapEditorTool {

	private MapController mapController;
	private MapEventType type = MapEventType.ShipGeneration;


	public MapEventTool(MapController mapController) {
		this.mapController = mapController;
	}

	public MapEventType Type { 
		get { return type; }
		set { type = value; }
	}

	public void Destory() {
	}

	public ShipGenerationEvent CreateShipGenerationEvent(Vector2 position) { 
		ShipGenerationEvent mapEvent = new ShipGenerationEvent();
		mapEvent.X = position.x;
		mapEvent.Y = position.y;
		mapEvent.Time = mapController.Map.StartTime;
		return mapEvent;
	}

	public OilSpillingEvent CreateOilSpillingEvent(Vector2 position) {
		OilSpillingEvent mapEvent = new OilSpillingEvent();
		mapEvent.X = position.x;
		mapEvent.Y = position.y;
		mapEvent.Time = mapController.Map.StartTime;
		return mapEvent;
	}

	public MapEvent CreateMapEvent(Vector2 position) {
		switch (type) {
		case MapEventType.OilSpilling:
			return CreateOilSpillingEvent(position);

		case MapEventType.ShipGeneration:
			return CreateShipGenerationEvent(position);
		}
		return null;
	}

	public void RespondMouseLeftClick() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.collider != null && ray.collider.tag == "Background") {
			MapEvent mapEvent = CreateMapEvent(ray.point);
			GameObject mapEventGO = mapController.AddMapEvent(mapEvent);
			GameObject.Find("MapEditorController").GetComponent<MapEditorController>().SelectOne(mapEventGO.GetComponent<MapEventVO>());
		} else if (ray.collider != null && ray.collider.tag == "Event") {
			GameObject.Find("MapEditorController").GetComponent<MapEditorController>().SelectOne(ray.collider.gameObject.GetComponent<MapEventVO>());
		}
	}

	public void RespondMouseLeftUp() {
	}

	public void RespondMouseMove(float x, float y) {
	}

	public void RespondMouseRightClick() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.collider != null && ray.collider.tag == "Event") {
			mapController.RemoveMapEvent (ray.collider.gameObject);
		}
	}

	public bool CanDestroy() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.collider != null && ray.collider.tag == "Event") {
			return false;
		}
		return true;
	}
}
