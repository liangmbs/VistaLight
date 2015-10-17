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
	public GameObject Dots;

	public GameObject PetroImage;
	public GameObject BreakImage;
	public GameObject BulkImage;
	public GameObject PortImage;

	public Button SaveMapButton;
	public List<Button> buttons = new List<Button> (); 

	public int property;
	public int dock;

	// public SingleDirectionTool singleDirectionTool;

	public IMapEditorTool mapEditorTool;


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

		SaveMapButton.GetComponent<Button> ().onClick.AddListener (() => {SaveMap();});

		buttons.Add (SingleDirectionLane);
		buttons.Add (BiDirectionalLane);
		buttons.Add (Intersection);
		buttons.Add(MoveCameraButton);
		buttons.Add (PetroDock);
		buttons.Add (BreakbulkDock);
		buttons.Add (BulkDock);
		buttons.Add (Port);

		SelectTool("MoveCamera", MoveCameraButton);
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown(0)) {

			// If the event is responded by UI elements, do not respond again.
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			// Respond click
			if (mapEditorTool != null)
				mapEditorTool.RespondMouseClick();
		}

		// print(string.Format("Mouse {0}, {1}", Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
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
			mapEditorTool = new SingleDirectionTool(GameObject.Find("Map").GetComponent<Map>());
			break;

		case "MoveCamera":
			mapEditorTool = new MoveTool();
			break;
			
		case "BiDirectional":
			property = 2;
			break;
			
		case "Intersection":
			property = 3;
			break;
		case "Petro":
			property = 4;
			break;
			
		case "Break":
			property = 5;
			break;
			
		case "Bulk":
			property = 6;
			break;
			
		case "Port":
			property = 7;
			break;
		}

	}

	public void SaveMap() {
		Map map = GameObject.Find ("Map").GetComponent<Map> ();
		MapStringifier mapStringifier = new MapStringifier (map);
		System.IO.File.WriteAllText ("map.json", mapStringifier.Stringify ());
	}

}
