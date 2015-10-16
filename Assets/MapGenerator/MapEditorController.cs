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

	public SingleDirectionTool singleDirectionTool;


	enum Dockstypes{
		Petro, Breakbulk, Bulk, Port
	}


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
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			print ("Dot generator.update\n");

			// If the event is responded by UI elements, do not respond again.
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			// Which button is on
			if (property == 1) {
				print(singleDirectionTool);
				singleDirectionTool.RespondMouseClick ();
			}
		}
		/*
		if (Input.GetMouseButtonDown (0)) {

			if (EventSystem.current.IsPointerOverGameObject())
				return;

			// Get the mouse pointer is in map
			Node previousNode = null;
			RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(ray.collider != null && property <=3 && DotID == 0){
				GameObject CreatedDot = Instantiate (Dots, ray.point, transform.rotation) as GameObject;

				// Fixme, the coordination need transformation for the real map\
				Node firstNode = new Node(ray.point);
				print (this.map);
				map.addNode(firstNode);
				previousNode = firstNode;

				CreatedDots.Add (CreatedDot);
				CreatedDots [DotID].name = DotID.ToString();	
				DotID++;
			}

			// Create port

			if(ray.collider != null && property > 3 ){
				switch (property){

				case 4:
					GameObject CreatedPort = Instantiate (PetroImage, ray.point, transform.rotation) as GameObject;
					break;

				case 5:
					GameObject CreatedPort1 = Instantiate (BreakImage, ray.point, transform.rotation) as GameObject;
					break;

				case 6:
					GameObject CreatedPort2 = Instantiate (BulkImage, ray.point, transform.rotation) as GameObject;
					break;

				case 7:
					GameObject CreatedPort3 = Instantiate (PortImage, ray.point, transform.rotation) as GameObject;
					break;
				}
			}


			// 
			if(DotID > 1){

				//print (CreatedDots.Count);
				Vector3 objectLine = (CreatedDots[DotID-1].transform.position - CreatedDots[DotID-2].transform.position);
				//print ("Here\n");
				float segmentDistance = 2.0f;
				float distance = objectLine.magnitude;
				//print (distance);
				float remainingDistance = distance;
				Vector3 previousDot = CreatedDots[DotID - 2].transform.position	;
				while (remainingDistance > segmentDistance) {
					Vector3 nextPoint = previousDot + objectLine.normalized * segmentDistance;
					Node nextNode = new Node(nextPoint);
					map.addNode(nextNode);

					if (property == 1) { 
						map.addConnection(previousNode, nextNode, false);
					} else if (property == 2){
						map.addConnection(previousNode, nextNode, true);
					}

					previousNode = nextNode;
					previousDot = nextPoint;
					remainingDistance -= segmentDistance;
					GameObject line = Instantiate (Line, nextPoint, Quaternion.identity) as GameObject;
				}
			}
		}
		*/
		
	}
	/*


	public void getButton(string mission){

		switch (mission) {

		case "SingleDirectional":
			property = 1;
			break;

		case "BiDirectional":
			property = 2;
			break;

		case "Intersection":
			property = 3;
			break;
		}

		print (property);
	}

	public void getPort(string mission){
		
		switch (mission) {
			
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
	}*/

	public void selectTool(string selected, Button selectedButton){
		foreach (Button element in buttons) {
			element.image.color = Color.gray;
		}
		selectedButton.image.color = Color.white;

		switch(selected){
		case "SingleDirectional":
			property = 1;
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
