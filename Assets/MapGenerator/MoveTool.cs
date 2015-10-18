using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MoveTool : IMapEditorTool {

	private bool isDragging = false;
	private Vector2 previousPoint = new Vector2(0, 0);

	public void Destory() {
		// Nothing to do in this function
	}

	public void RespondMouseLeftClick() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.collider != null && ray.collider.tag == "Background") {
			isDragging = true;
			previousPoint = ray.point;
		}
	}

	public void RespondMouseLeftUp() {
		isDragging = false;
	}

	public void RespondMouseMove(float x, float y) {
		if (isDragging) {
			RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			Vector2 vector = ray.point - previousPoint;
			Vector3 cameraPosition = GameObject.Find("Main Camera").transform.position;
			GameObject.Find("Main Camera").transform.position =
				new Vector3(cameraPosition.x - vector.x, cameraPosition.y - vector.y, -10);
		}
	}

}

