using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MoveTool : IMapEditorTool {
	public void Destory() {
		// Nothing to do in this function
	}

	public void RespondMouseClick() {
	}

	public void RespondMouseMove(float x, float y) {
		if (Input.GetMouseButton(0)) {
			Vector3 cameraPosition = GameObject.Find("Main Camera").transform.position;
			GameObject.Find("Main Camera").transform.position =
				new Vector3(cameraPosition.x + x, cameraPosition.y + y, -10);
		}
	}

}

