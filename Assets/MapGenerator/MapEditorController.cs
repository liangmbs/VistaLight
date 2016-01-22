using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MapEditorController : MonoBehaviour {

	public MapController mapController;

	private List<Button> ToolButtons = new List<Button>();
	public Button MoveCameraButton;
	public Button UnidirectionalLaneButton;
	public Button BidirectionalLaneButton;
	public Button PetroButton;
	public Button BreakBulkButton;
	public Button BulkButton;
	public Button PortButton;
	public Button EventButton;


	public GameObject mapInfoSidePanel;

	public GameObject shipPanel;

	public IMapEditorTool mapEditorTool;


	// Use this for initialization
	void Start() {
		ToolButtons.Add(MoveCameraButton);
		ToolButtons.Add(UnidirectionalLaneButton);
		ToolButtons.Add(BidirectionalLaneButton);
		ToolButtons.Add(PetroButton);
		ToolButtons.Add(BreakBulkButton);
		ToolButtons.Add(BulkButton);
		ToolButtons.Add(PortButton);
		ToolButtons.Add(EventButton);

		SelectTool("MoveCamera");
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

	public void DeselectAllToolButtons() {
		foreach (Button button in ToolButtons) {
			button.image.color = Color.gray;
		}
	}

	public void SelectButton(Button button) {
		button.image.color = Color.white;	
	}

	public void DestroyCurrentTool() { 
		if (mapEditorTool != null) {
			mapEditorTool.Destory();
			mapEditorTool = null;
		}
	}

	public void SelectTool(string toolName) {
		DeselectAllToolButtons();
		DestroyCurrentTool();

		switch (toolName) {

		case "MoveCamera":
			mapEditorTool = new MoveTool();
			SelectButton(MoveCameraButton);
			break;

		case "UnidirectionalLane":
			RoadTool unidirectionTool = new RoadTool(GameObject.Find("Map").GetComponent<MapController>());
			mapEditorTool = unidirectionTool;
			unidirectionTool.BiDirection = false;
			SelectButton(UnidirectionalLaneButton);
			break;


		case "BidirectionalLane":
			RoadTool bidirectionTool = new RoadTool(GameObject.Find("Map").GetComponent<MapController>());
			mapEditorTool = bidirectionTool;
			bidirectionTool.BiDirection = true;
			SelectButton(BidirectionalLaneButton);
			break;

		case "Petro":
			mapEditorTool = new CreateDockTool(mapController, DockType.Petro);
			SelectButton(PetroButton);
			break;

		case "BreakBulk":
			mapEditorTool = new CreateDockTool(mapController, DockType.BreakBulk);
			SelectButton(BreakBulkButton);
			break;

		case "Bulk":
			mapEditorTool = new CreateDockTool(mapController, DockType.Bulk);
			SelectButton(BulkButton);
			break;

		case "Port":
			mapEditorTool = new CreateDockTool(mapController, DockType.Port);
			SelectButton(PortButton);
			break;

		case "Event":
			mapEditorTool = new MapEventTool(mapController);
			SelectButton(EventButton);
			break;
		}
	}

	public void ShowShipPanel() {
		shipPanel.SetActive(true);
	}

	public void CloseShipPanel() {
		shipPanel.SetActive(false);
	}

}
