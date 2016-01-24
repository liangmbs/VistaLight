using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UnidirectionButtonController : ToolButtonController {

	public MapController mapController;
	public ToolSelector toolSelector;

	public override void SelectTool() {
		RoadTool tool = new RoadTool(mapController);
		tool.BiDirection = false;
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
