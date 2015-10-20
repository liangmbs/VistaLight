using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MapEditorController : MonoBehaviour {

	public int DotID = 0;
	public List<GameObject> CreatedDots= new List<GameObject>();
	
	public Button SingleDirectionLane;
	public Button BiDirectionalLane;
	public Button Intersection;
	public Button MoveCameraButton;

	public Button PetroDock;
	public Button BreakbulkDock;
	public Button BulkDock;
	public Button Port;

	public GameObject mapInfoPanel;

	public List<Button> buttons = new List<Button> (); 

	public int property;
	public int dock;

	// public SingleDirectionTool singleDirectionTool;

	public IMapEditorTool mapEditorTool;


	enum Dockstypes{
		Petro, Breakbulk, Bulk, Port
	}


	// Use this for initialization
	void Start () {

		MoveCameraButton.GetComponent<Button>().onClick.AddListener(() => {
			SelectTool("MoveCamera", MoveCameraButton);});
		SingleDirectionLane.GetComponent<Button> ().onClick.AddListener (() => {
			SelectTool("SingleDirectional",SingleDirectionLane);});
		BiDirectionalLane.GetComponent<Button> ().onClick.AddListener (() => {
			SelectTool ("BiDirectional",BiDirectionalLane);});
		Intersection.GetComponent<Button> ().onClick.AddListener (() => {
			SelectTool ("Intersection",Intersection);});
		PetroDock.GetComponent<Button> ().onClick.AddListener (() => {
			SelectTool ("Petro",PetroDock);});
		BreakbulkDock.GetComponent<Button> ().onClick.AddListener (() => {
			SelectTool ("Break",BreakbulkDock);});
		BulkDock.GetComponent<Button> ().onClick.AddListener (() => {
			SelectTool ("Bulk",BulkDock);});
		Port.GetComponent<Button> ().onClick.AddListener (() => {
			SelectTool ("Port",Port);});

		buttons.Add (SingleDirectionLane);
		buttons.Add (BiDirectionalLane);
		buttons.Add (Intersection);
		buttons.Add(MoveCameraButton);
		buttons.Add (PetroDock);
		buttons.Add (BreakbulkDock);
		buttons.Add (BulkDock);
		buttons.Add (Port);

		SelectTool("MoveCamera", MoveCameraButton);

		// Set the panel to new map panel
		mapInfoPanel.transform.SetAsLastSibling();
	}

	// Update is called once per frame
	void Update() {
		// Mouse left click
		if (Input.GetMouseButtonDown(0)) {
			// If the event is responded by UI elements, do not respond again.
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			// Respond click
			if (mapEditorTool != null)
				mapEditorTool.RespondMouseLeftClick();
		}

		// Mouse left click up
		if (Input.GetMouseButtonUp(0)) {
			// If the event is responded by UI elements, do not respond again.
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			// Respond click
			if (mapEditorTool != null)
				mapEditorTool.RespondMouseLeftUp();
		}

		// Mouse right click
		if (Input.GetMouseButtonDown(1)) {
			// If the event is responded by UI elements, do not respond again.
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			// Respond click
			if (mapEditorTool != null)
				mapEditorTool.RespondMouseRightClick();
		}

		// Mouse move
		if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
			if (mapEditorTool != null) {
				mapEditorTool.RespondMouseMove(
					Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
			}
		}
	}


	public void SelectTool(string selected, Button selectedButton){
		foreach (Button element in buttons) {
			element.image.color = Color.gray;
		}
		selectedButton.image.color = Color.white;

		// Destory the tool if it is initialized
		if (mapEditorTool != null) {
			mapEditorTool.Destory();
		}

		switch(selected){
		case "SingleDirectional":
			// property = 1;
			RoadTool unidirectionTool = new RoadTool(GameObject.Find("Map").GetComponent<Map>());
			mapEditorTool = unidirectionTool;
			unidirectionTool.BiDirection = false;
			break;

		case "MoveCamera":
			mapEditorTool = new MoveTool();
			break;
			
		case "BiDirectional":
			RoadTool bidirectionTool = new RoadTool(GameObject.Find("Map").GetComponent<Map>());
			mapEditorTool = bidirectionTool;
			bidirectionTool.BiDirection = true;
			break;
			
		case "Intersection":
			property = 3;
			break;

		case "Petro":
			mapEditorTool = new CreateDockTool(GameObject.Find("Map").GetComponent<Map>(), DockType.Petro);
			break;
			
		case "Break":
			mapEditorTool = new CreateDockTool(GameObject.Find("Map").GetComponent<Map>(), DockType.BreakBulk);
			break;
			
		case "Bulk":
			mapEditorTool = new CreateDockTool(GameObject.Find("Map").GetComponent<Map>(), DockType.Bulk);
			break;
			
		case "Port":
			mapEditorTool = new CreateDockTool(GameObject.Find("Map").GetComponent<Map>(), DockType.Port);
			break;
		}

	}

	public void SaveMap() {
		Map map = GameObject.Find ("Map").GetComponent<Map> ();
		MapStringifier mapStringifier = new MapStringifier (map);
		System.IO.File.WriteAllText ("map.json", mapStringifier.Stringify ());
	}

}
