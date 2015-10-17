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
	public GameObject Line;

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


		SingleDirectionLane.GetComponent<Button> ().onClick.AddListener (() => {
			 selectTool("SingleDirectional",SingleDirectionLane);});
		BiDirectionalLane.GetComponent<Button> ().onClick.AddListener (() => {
			selectTool ("BiDirectional",BiDirectionalLane);});
		Intersection.GetComponent<Button> ().onClick.AddListener (() => {
			selectTool ("Intersection",Intersection);});
		PetroDock.GetComponent<Button> ().onClick.AddListener (() => {
			selectTool ("Petro",PetroDock);});
		BreakbulkDock.GetComponent<Button> ().onClick.AddListener (() => {
			selectTool ("Break",BreakbulkDock);});
		BulkDock.GetComponent<Button> ().onClick.AddListener (() => {
			selectTool ("Bulk",BulkDock);});
		Port.GetComponent<Button> ().onClick.AddListener (() => {
			selectTool ("Port",Port);});

		SaveMapButton.GetComponent<Button> ().onClick.AddListener (() => {SaveMap();});

		buttons.Add (SingleDirectionLane);
		buttons.Add (BiDirectionalLane);
		buttons.Add (Intersection);
		buttons.Add (PetroDock);
		buttons.Add (BreakbulkDock);
		buttons.Add (BulkDock);
		buttons.Add (Port);
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown(0)) {

			// If the event is responded by UI elements, do not respond again.
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			// Respond click
			mapEditorTool.RespondMouseClick();
		}
	}


	public void selectTool(string selected, Button selectedButton){
		foreach (Button element in buttons) {
			element.image.color = Color.gray;
		}
		selectedButton.image.color = Color.white;

		switch(selected){
		case "SingleDirectional":
			// property = 1;
			mapEditorTool = new SingleDirectionTool(GameObject.Find("Map").GetComponent<Map>());
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
