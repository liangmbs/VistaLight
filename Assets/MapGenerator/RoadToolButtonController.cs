using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class RoadToolButtonController : ToolButtonController {

	public MapController mapController;
	public ToolSelector toolSelector;

	public override void SelectTool(string setting) {
		RoadTool tool = new RoadTool(mapController);
		if (setting == "unidirectional") {
			tool.BiDirection = false;
		} else {
			tool.BiDirection = true;
		}
		toolSelector.SelectTool(tool);
		gameObject.GetComponent<Button>().image.color = Color.white;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
