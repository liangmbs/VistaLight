using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MapEventToolButtonController : ToolButtonController {

	public MapController mapController;
	public ToolSelector toolSelector;
	public MapEventType type;

	public override void SelectTool(string setting) {
		MapEventTool tool = new MapEventTool(mapController);
		toolSelector.SelectTool(tool);
		tool.Type = type;
		gameObject.GetComponent<Button>().image.color = Color.white;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
