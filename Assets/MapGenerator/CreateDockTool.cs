using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CreateDockTool : IMapEditorTool {
	private DockType type;
	private MapController mapController;

	public CreateDockTool(MapController map, DockType type) {
		this.type = type;
		this.mapController = map;
	}

	public DockType Type {
		get { return type; }
		set { type = value; }
	}

	public void Destory() {
	}

	public void RespondMouseLeftClick() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.collider != null && ray.collider.tag == "Node") {
			mapController.AddDock(ray.collider.gameObject, type);
		}
	}

	public void RespondMouseLeftUp() {
	}

	public void RespondMouseMove(float x, float y) {
	}

	public void RespondMouseRightClick() {
	}
}

