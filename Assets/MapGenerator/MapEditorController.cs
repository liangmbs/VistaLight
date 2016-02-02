using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MapEditorController : MonoBehaviour {

	public MapController mapController;

	public GameObject mapInfoSidePanel;

	public GameObject shipPanel;

	public ToolSelector toolSelector;

	private MapSelectableVO selected;
    private GameObject sidePanelInDisplay;

	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		IMapEditorTool mapEditorTool = toolSelector.toolSelected;
		
		// Mouse left click
		if (Input.GetMouseButtonDown(0)) {
			// If the event is responded by UI elements, do not respond again.
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			// Respond click
			if (mapEditorTool != null) {
				mapEditorTool.RespondMouseLeftClick();
			} else {
				SelectClicked();
			}
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
			if (mapEditorTool != null) {
				if(mapEditorTool.CanDestroy()) {
					toolSelector.DeselectCurrentTool();
				} else {
					mapEditorTool.RespondMouseRightClick();
				}
			} else {
				DeselectAll();
			}
		}

		// Mouse move
		if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
			if (mapEditorTool != null) {
				mapEditorTool.RespondMouseMove(
					Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
			}
		}
	}

	private void SelectClicked() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (ray.collider == null) return;
		Debug.Log(ray.collider.tag);

		// Select thing
		if (ray.collider.tag == "Node") {
			SelectOne(ray.collider.gameObject.GetComponent<NodeVO>());
		} else if (ray.collider.tag == "Dock") {
			SelectOne(ray.collider.gameObject.GetComponent<DockVO>());
		} else if (ray.collider.tag == "Event") {
			SelectOne(ray.collider.gameObject.GetComponent<MapEventVO>());
		}
	}

	public void SelectOne(MapSelectableVO vo) {
		DeselectAll();
		vo.Select();
		selected = vo;

        GameObject sidePanel = vo.GetSidePanel();
        if (sidePanel != null) {
            sidePanel.SetActive(true);
            sidePanelInDisplay = sidePanel;
        }
	}

	public void DeselectAll() {
		if (selected != null) {
			selected.Deselect();
			selected = null;
		}

        if (sidePanelInDisplay != null) {
            sidePanelInDisplay.SetActive(false);
            sidePanelInDisplay = null;
        }
	}

	public void ShowShipPanel() {
		shipPanel.SetActive(true);
	}

	public void CloseShipPanel() {
		shipPanel.SetActive(false);
	}

}
