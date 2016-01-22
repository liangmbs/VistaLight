using System;
using UnityEngine;

class MapEventTool : IMapEditorTool {

	private MapController mapController;

	public MapEventTool(MapController mapController) {
		this.mapController = mapController;
	}

	public void Destory() {
	}

	public void RespondMouseLeftClick() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.collider != null && ray.collider.tag == "Background") {
			mapController.CreateMapEvent(ray.point);
		}
	}

	public void RespondMouseLeftUp() {
	}

	public void RespondMouseMove(float x, float y) {
	}

	public void RespondMouseRightClick() {
	}
}
